using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICatalogTreeService
    {
        Task<CatalogTree> GetCatalogHierarchyTreeByEnterpriseId(int enterpriseId);
        Task<CatalogTree> GetCatalogHierarchyByCode(int enterpriseId, string code);
    }
}