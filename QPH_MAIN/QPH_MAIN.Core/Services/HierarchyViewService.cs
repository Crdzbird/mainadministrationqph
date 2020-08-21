using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class HierarchyViewService : IUserViewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HierarchyViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserView> GetHierarchy(int parentId) => await _unitOfWork.UserViewRepository.GetHierarchyByParent(parentId);

        public async Task InsertHierarchy(UserView hierarchyView)
        {
            await _unitOfWork.UserViewRepository.Add(hierarchyView);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateHierarchyView(UserView hierarchyView)
        {
            var existingView = await _unitOfWork.UserViewRepository.GetById(hierarchyView.Id);
            existingView.parent = hierarchyView.parent;
            existingView.children = hierarchyView.children;
            _unitOfWork.UserViewRepository.Update(existingView);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHierarchyView(int id)
        {
            await _unitOfWork.UserViewRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task RemoveByUserId(int userId) => await _unitOfWork.UserViewRepository.RemoveByUserId(userId);
    }
}