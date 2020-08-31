using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class EnterpriseHierarchyCatalogRepository : BaseRepository<EnterpriseHierarchyCatalog>, IEnterpriseHierarchyCatalogRepository
    {
        public EnterpriseHierarchyCatalogRepository(QPHContext context) : base(context) { }
        public async Task<EnterpriseHierarchyCatalog> GetHierarchyByParent(int parentId) => await _entities.FirstOrDefaultAsync(x => x.parent == parentId);
        public async Task<EnterpriseHierarchyCatalog> GetHierarchyByChildren(int childrenId) => await _entities.FirstOrDefaultAsync(x => x.children == childrenId);
        public async Task RemoveByEntepriseId(int enterpriseId) => _entities.RemoveRange(await _entities.Where(u => u.id_enterprise == enterpriseId).ToListAsync());
    }
}