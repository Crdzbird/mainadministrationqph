using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICatalogService
    {
        PagedList<Catalog> GetCatalogs(CatalogQueryFilter filters);
        Task<Catalog> GetCatalog(int id);
        Task InsertCatalog(Catalog views);
        Task<bool> RebuildHierarchy(CatalogTree tree, int idEnterprise);
        Task<bool> UpdateCatalog(Catalog views);
        Task<bool> DeleteCatalog(int id);
        Task DeleteHierarchyByEnterpriseId(int enterpriseId);
    }
}