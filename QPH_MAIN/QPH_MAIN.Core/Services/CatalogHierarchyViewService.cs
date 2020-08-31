using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class CatalogHierarchyViewService : IEnterpriseHierarchyCatalogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogHierarchyViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EnterpriseHierarchyCatalog> GetHierarchy(int parentId) => await _unitOfWork.EnterpriseHierarchyCatalogRepository.GetHierarchyByParent(parentId);

        public async Task InsertHierarchy(EnterpriseHierarchyCatalog enterpriseHierarchyCatalog)
        {
            await _unitOfWork.EnterpriseHierarchyCatalogRepository.Add(enterpriseHierarchyCatalog);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateHierarchyCatalog(EnterpriseHierarchyCatalog enterpriseHierarchyCatalog)
        {
            var existingCatalog = await _unitOfWork.EnterpriseHierarchyCatalogRepository.GetById(enterpriseHierarchyCatalog.Id);
            existingCatalog.parent = enterpriseHierarchyCatalog.parent;
            existingCatalog.children = enterpriseHierarchyCatalog.children;
            _unitOfWork.EnterpriseHierarchyCatalogRepository.Update(existingCatalog);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHierarchyCatalog(int id)
        {
            await _unitOfWork.EnterpriseHierarchyCatalogRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task RemoveByEnterpriseId(int enterpriseId) => await _unitOfWork.EnterpriseHierarchyCatalogRepository.RemoveByEntepriseId(enterpriseId);
    }
}