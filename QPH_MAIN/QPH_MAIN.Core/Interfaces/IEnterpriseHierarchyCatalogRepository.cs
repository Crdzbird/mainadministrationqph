using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IEnterpriseHierarchyCatalogRepository : IRepository<EnterpriseHierarchyCatalog>
    {
        Task<EnterpriseHierarchyCatalog> GetHierarchyByParent(int parentId);
        Task<EnterpriseHierarchyCatalog> GetHierarchyByChildren(int childrenId);
        Task RemoveByEntepriseId(int enterpriseId);
    }
}