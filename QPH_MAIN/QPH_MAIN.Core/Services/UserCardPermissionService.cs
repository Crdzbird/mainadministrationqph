using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class UserCardPermissionService : IUserCardPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserCardPermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserCardPermission> GetUserCardPermission(int id) => await _unitOfWork.UserCardPermissionRepository.GetById(id);

        public async Task InsertUserCardPermission(UserCardPermission userCardPermission)
        {
            if (await _unitOfWork.UserCardGrantedRepository.GetById(userCardPermission.id_card_granted) == null) throw new BusinessException("Card doesn't exist");
            if (await _unitOfWork.PermissionsRepository.GetById(userCardPermission.id_permission) == null) throw new BusinessException("Permission doesn't exist");
            await _unitOfWork.UserCardPermissionRepository.Add(userCardPermission);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserCardPermission(UserCardPermission userCardPermission)
        {
            var existingUserCardPermission = await _unitOfWork.UserCardPermissionRepository.GetById(userCardPermission.Id);
            existingUserCardPermission.id_card_granted =  userCardPermission.id_card_granted;
            existingUserCardPermission.id_permission = userCardPermission.id_permission;
            _unitOfWork.UserCardPermissionRepository.Update(existingUserCardPermission);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserCardPermission(int id)
        {
            await _unitOfWork.UserCardPermissionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task DeletePermissionByUserId(int userId)
        {
            await _unitOfWork.UserCardPermissionRepository.RemoveByUserId(userId);
        }
    }
}