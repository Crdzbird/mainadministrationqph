using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UserViewRepository : BaseRepository<UserView>, IUserViewRepository
    {
        public UserViewRepository(QPHContext context) : base(context) { }
        public async Task<UserView> GetHierarchyByChildren(int childrenId) => await _entities.FirstOrDefaultAsync(x => x.children == childrenId);
        public async Task<UserView> GetHierarchyByParent(int parentId) => await _entities.FirstOrDefaultAsync(x => x.parent == parentId);
        public async Task RemoveByUserId(int userId) => _entities.RemoveRange(await _entities.Where(u => u.userId == userId).ToListAsync());
    }
}