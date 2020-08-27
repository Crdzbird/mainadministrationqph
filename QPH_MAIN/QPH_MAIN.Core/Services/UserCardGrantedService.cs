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
    public class UserCardGrantedService : IUserCardGrantedService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserCardGrantedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserCardGranted> GetUserCardGranted(int id) => await _unitOfWork.UserCardGrantedRepository.GetById(id);

        public async Task InsertUserCardGranted(UserCardGranted userCardGranted)
        {
            if (await _unitOfWork.UserRepository.GetById(userCardGranted.id_user) == null) throw new BusinessException("User doesn't exist");
            if (await _unitOfWork.CardsRepository.GetById(userCardGranted.id_card) == null) throw new BusinessException("Card doesn't exist");
            await _unitOfWork.UserCardGrantedRepository.Add(userCardGranted);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserCardGranted(UserCardGranted userCardGranted)
        {
            var existingUserCardGranted = await _unitOfWork.UserCardGrantedRepository.GetById(userCardGranted.Id);
            existingUserCardGranted.id_card =  userCardGranted.id_card;
            existingUserCardGranted.id_user = userCardGranted.id_user;
            _unitOfWork.UserCardGrantedRepository.Update(existingUserCardGranted);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserCardGranted(int id)
        {
            await _unitOfWork.UserCardGrantedRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task DeleteUserCardGrantedByUserId(int userId) => await _unitOfWork.UserCardGrantedRepository.RemoveByUserId(userId);
    }
}