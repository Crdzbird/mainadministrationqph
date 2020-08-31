using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IEnterpriseHierarchyCatalogService
    {
        Task<EnterpriseHierarchyCatalog> GetHierarchy(int id);
        Task InsertHierarchy(EnterpriseHierarchyCatalog enterpriseHierarchyCatalog);
        Task<bool> UpdateHierarchyCatalog(EnterpriseHierarchyCatalog enterpriseHierarchyCatalog);
        Task<bool> DeleteHierarchyCatalog(int id);
        Task RemoveByEnterpriseId(int userId);
    }
}