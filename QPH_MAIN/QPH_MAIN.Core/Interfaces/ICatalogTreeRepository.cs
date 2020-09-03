using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICatalogTreeRepository : IRepository<CatalogTree>
    {
        Task<CatalogTree> GetCatalogTreeByEnterpriseId(int enterpriseId);
        Task<CatalogTree> GetCatalogHierarchyByCode(int enterpriseId, string code);
    }
}