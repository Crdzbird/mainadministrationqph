using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class CatalogTreeRepository : BaseRepository<CatalogTree>, ICatalogTreeRepository
    {
        QPHContext _context; 
        public CatalogTreeRepository(QPHContext context) : base(context) {
            this._context = context;
        }

        public async Task<CatalogTree> GetCatalogHierarchyByCode(int enterpriseId, string code)
        {
            var result = await _entities.FromSqlRaw("exec HierarchyCatalogByCodeNew @code={0}, @idEnterprise={1}", code, enterpriseId).ToListAsync();
            if (result == null || result.Count == 0) return null;
            var parentRoot = _context.Set<CatalogTree>().FromSqlRaw("exec RootCatalogByCodeEnterprise @code={0}, @idEnterprise={1}", code, enterpriseId).AsNoTracking().AsEnumerable().FirstOrDefault();
            result.Add(parentRoot);
            Dictionary<int, CatalogTree> dict = result.ToDictionary(loc => loc.son, loc => new CatalogTree { son = loc.son, code = loc.code, parent = loc.parent, title = loc.title, Id = loc.son });
            foreach (CatalogTree loc in dict.Values)
            {
                if (loc.parent != loc.Id)
                {
                    CatalogTree parent = dict[loc.parent];
                    parent.Children.Add(loc);
                }
            }
            CatalogTree root = dict.Values.First(loc => loc.parent == loc.Id);
            return root;
        }

        public async Task<CatalogTree> GetCatalogTreeByEnterpriseId(int enterpriseId)
        {
            var result = await _entities.FromSqlRaw("exec HierarchyCatalogByEnterpriseNew @idEnterprise={0}", enterpriseId).ToListAsync();
            if (result == null || result.Count == 0) return null;
            var parentRoot = await _entities.FromSqlRaw("select top 1 id as parent, 'catalog' as title, 1 as children, id as Id, '' as code from Catalog where code = 'catalogo'").FirstOrDefaultAsync();
            result.Add(parentRoot);
            Dictionary<int, CatalogTree> dict = result.ToDictionary(loc => loc.son, loc => new CatalogTree { son = loc.son, code = loc.code, parent = loc.parent, title = loc.title, Id = loc.son });
            foreach (CatalogTree loc in dict.Values)
            {
                if (loc.parent != loc.Id)
                {
                    CatalogTree parent = dict[loc.parent];
                    parent.Children.Add(loc);
                }
            }
            CatalogTree root = dict.Values.First(loc => loc.parent == loc.Id);
            return root;
        }
    }
}