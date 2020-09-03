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
    public class TreeRepository : BaseRepository<Tree>, ITreeRepository
    {
        QPHContext _context; 
        public TreeRepository(QPHContext context) : base(context) {
            this._context = context;
        }

        public async Task<Tree> GetTreeByUserId(int userId)
        {
            var result = await _entities.FromSqlRaw("exec HierarchyViewByUserNew @idUser={0}", userId).ToListAsync();
            if (result == null || result.Count == 0) return null;
            var parentRoot = await _entities.FromSqlRaw("select top 1 id as parent, 'root' as title, '' as route, 1 as children, id as Id from Views where code = 'root'").FirstOrDefaultAsync();
            result.Add(parentRoot);
            Dictionary<int, Tree> dict = result.ToDictionary(loc => loc.son, loc => new Tree { son = loc.son, route = loc.route, parent = loc.parent, title = loc.title, Id = loc.son });
            foreach (Tree loc in dict.Values)
            {
                var cards = await _context.Cards.FromSqlRaw("exec BuildCardsByView @idView={0}", loc.son).ToListAsync();
                List<CardsPermissionStatus> listCardsPermissions = new List<CardsPermissionStatus>();
                if(cards != null)
                {
                    foreach (Cards card in cards)
                    {
                        listCardsPermissions.Add(new CardsPermissionStatus
                        {
                            Id = card.Id,
                            card = card.card
                        });
                    }
                }
                loc.cards = listCardsPermissions;
                loc.permissions = await _context.PermissionStatuses.FromSqlRaw("exec PermissionStatus @idUser = {0}, @idView = {1}", userId, loc.son).ToListAsync();
                if (loc.parent != loc.Id)
                {
                    Tree parent = dict[loc.parent];
                    parent.Children.Add(loc);
                }
            }
            Tree root = dict.Values.First(loc => loc.parent == loc.Id);
            return root;
        }
    }
}