using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class CatalogRepository : BaseRepository<Catalog>, ICatalogRepository
    {
        public CatalogRepository(QPHContext context) : base(context) { }

        public async Task<Catalog> GetCatalogNameByHierarchyId(int catalogId) => await _entities.FirstOrDefaultAsync(x => x.Id == catalogId);
    }
}