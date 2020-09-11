using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.DTOs;
using OrderByExtensions;

namespace QPH_MAIN.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public UserService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<User> GetUser(int id) => await _unitOfWork.UserRepository.GetById(id);

        public PagedList<User> GetUsers(UserQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var users = _unitOfWork.UserRepository.GetAll();
            if (filters.filter != null)
            {
                users = users.Where(x => x.nickname.ToLower().Contains(filters.filter.ToLower()));
                users = users.Where(x => x.email.ToLower().Contains(filters.filter.ToLower()));
                users = users.Where(x => x.phone_number.ToLower().Contains(filters.filter.ToLower()));
                users = users.Where(x => x.firstName.ToLower().Contains(filters.filter.ToLower()));
                users = users.Where(x => x.lastName.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.CountryId != null)
            {
                users = users.Where(x => x.id_country == filters.CountryId);
            }
            if (filters.RoleId != null)
            {
                users = users.Where(x => x.id_role == filters.RoleId);
            }
            if (filters.EnterpriseId != null)
            {
                users = users.Where(x => x.id_enterprise == filters.EnterpriseId);
            }
            if (filters.Nickname != null)
            {
                users = users.Where(x => x.nickname.ToLower().Contains(filters.Nickname.ToLower()));
            }
            if (filters.FirstName != null)
            {
                users = users.Where(x => x.firstName == filters.FirstName);
            }
            if (filters.LastName != null)
            {
                users = users.Where(x => x.lastName == filters.LastName);
            }
            if (filters.Email != null)
            {
                users = users.Where(x => x.email.ToLower().Contains(filters.Email.ToLower()));
            }
            if (filters.PhoneNumber != null)
            {
                users = users.Where(x => x.phone_number.ToLower().Contains(filters.PhoneNumber.ToLower()));
            }
            if (filters.Status != null)
            {
                users = users.Where(x => x.status.Equals(filters.Status));
            }
            if (filters.RoleId != null)
            {
                users = users.Where(x => x.id_role == filters.RoleId);
            }
            if (filters.EnterpriseId != null)
            {
                users = users.Where(x => x.id_enterprise == filters.EnterpriseId);
            }
            if (filters.CountryId != null)
            {
                users = users.Where(x => x.id_country == filters.CountryId);
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    users = users.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<User>.Create(users, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task<User> InsertUser(User user)
        {
            if (await _unitOfWork.RolesRepository.GetById(user.id_role) == null) throw new BusinessException("Role doesn't exist");
            if (await _unitOfWork.EnterpriseRepository.GetById(user.id_enterprise) == null) throw new BusinessException("Enterprise doesn't exist");
            if (await _unitOfWork.CountryRepository.GetById(user.id_country) == null) throw new BusinessException("Country doesn't exist");
            user.activation_code = GenerateActivationCode();
            user.status = false;
            user.is_account_activated = false;
            user.facebook_access_token = user.facebook_access_token ?? "";
            user.firebase_token = user.firebase_token ?? "";
            user.google_access_token = user.google_access_token ?? "";
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ActivateUserAccount(string code)
        {
            var userActivation = await _unitOfWork.UserRepository.GetUserByActivationCode(code);
            if (userActivation == null) return false;
            userActivation.activation_code = "";
            userActivation.status = true;
            userActivation.is_account_activated = true;
            _unitOfWork.UserRepository.Update(userActivation);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var existingUser = await _unitOfWork.UserRepository.GetById(user.Id);
            existingUser.id_role = user.id_role;
            existingUser.id_enterprise = user.id_enterprise;
            existingUser.id_country = user.id_country;
            existingUser.nickname = user.nickname;
            existingUser.firstName = user.firstName;
            existingUser.lastName = user.lastName;
            existingUser.email = user.email;
            existingUser.phone_number = user.phone_number;
            existingUser.hashPassword = user.hashPassword;
            existingUser.google_access_token = user.google_access_token;
            existingUser.facebook_access_token = user.facebook_access_token;
            existingUser.firebase_token = user.firebase_token;
            existingUser.is_account_activated = user.is_account_activated;
            existingUser.profile_picture = user.profile_picture;
            existingUser.status = user.status;
            _unitOfWork.UserRepository.Update(existingUser);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public void SendMail(string code, string destination, string message)
        {
            string from = "desarrolloqph1@qph.com.ec";
            string password = "Qph2020.";
            MailMessage mailMessage = new MailMessage(from, destination, "Account Activation", "<p>Account Acctivation: </p> " + message);
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(from, password);
            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }

        public string GenerateActivationCode(int length = 48, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length no puede ser menor a 0.");
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars no debe estar vacio.");
            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(string.Format("allowedChars puede contener no mas de {0} caracteres.", byteSize));
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }

        public async Task<bool> CheckActivationCode(string code)
        {
            var user = await _unitOfWork.UserRepository.GetUserByActivationCode(code);
            if (user == null) return false;
            user.activation_code = "";
            user.is_account_activated = true;
            user.status = true;
            _unitOfWork.UserRepository.Update(user);
            return true;
        }

        public async Task<User> GetLoginByCredentials(UserLogin userLogin) => await _unitOfWork.UserRepository.GetByUsername(userLogin.User);

        public async Task<UserDetailDto> GetUserDetail(int userId)
        {
            var existingUser = await _unitOfWork.UserRepository.GetDetailUser(userId);
            existingUser.enterprise.users = null;
            existingUser.country.users = null;
            existingUser.country.regions = null;
            existingUser.roles.users = null;
            return new UserDetailDto
            {
                Id_role = existingUser.roles.Id,
                Id_enterprise = existingUser.enterprise.Id,
                Id_country = existingUser.country.Id,
                Country = existingUser.country,
                Email = existingUser.email,
                Enterprise = existingUser.enterprise,
                Nickname = existingUser.nickname,
                Phone_number = existingUser.phone_number,
                Profile_picture = existingUser.profile_picture,
                Tree = await _unitOfWork.TreeRepository.GetTreeByUserId(userId)
            };
        }

        public async Task<bool> CheckDuplicatedEmail(string email) => await _unitOfWork.UserRepository.CheckDuplicatedEmail(email);

        public async Task<bool> CheckDuplicatedPhone(string phone) => await _unitOfWork.UserRepository.CheckDuplicatedPhone(phone);

        public async Task<bool> CheckDuplicatedNickname(string nickname) => await _unitOfWork.UserRepository.CheckDuplicatedNickname(nickname);
    }
}