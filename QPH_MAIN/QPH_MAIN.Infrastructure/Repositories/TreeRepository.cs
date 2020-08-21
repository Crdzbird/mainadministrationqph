using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class TreeRepository : BaseRepository<Tree>, ITreeRepository
    {
        public TreeRepository(QPHContext context) : base(context) { }

        public async Task<Tree> GetTreeByUserId(int userId)
        {
            var parentRoot = await _entities.FromSqlRaw("select top 1 id as parent, '' as title, 0 as children, id as Id from Views").FirstOrDefaultAsync();
            var result = await _entities.FromSqlRaw("exec HierarchyViewByUserNew @idUser={0}", userId).ToListAsync();
            Dictionary<int, Tree> dict = result.ToDictionary(loc => loc.son, loc => new Tree { son = loc.son, parent = loc.parent, title = loc.title, Id = loc.son });
            foreach (Tree loc in dict.Values)
            {
                if (loc.parent != parentRoot.parent)
                {
                    if (loc.son != loc.parent)
                    {
                        Tree parent = dict[loc.parent];
                        parent.Children.Add(loc);
                    }
                }
            }
            Tree root = dict.Values.First(loc => loc.son != parentRoot.parent);
            return root;
        }
    }
}