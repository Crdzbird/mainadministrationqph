using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class CatalogTreeService : ICatalogTreeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogTreeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CatalogTree> GetCatalogHierarchyTreeByEnterpriseId(int enterpriseId)
        {
            var tree = await _unitOfWork.CatalogTreeRepository.GetCatalogTreeByEnterpriseId(enterpriseId);
            return tree;
        }

        public async Task<CatalogTree> GetCatalogHierarchyByCode(int enterpriseId, string code)
        {
            var tree = await _unitOfWork.CatalogTreeRepository.GetCatalogHierarchyByCode(enterpriseId, code);
            return tree;
        }
    }
}