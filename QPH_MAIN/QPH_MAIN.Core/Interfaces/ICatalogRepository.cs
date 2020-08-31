using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICatalogRepository : IRepository<Catalog>
    {
        Task<Catalog> GetCatalogNameByHierarchyId(int catalogId);
    }
}