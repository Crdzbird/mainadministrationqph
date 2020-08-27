using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UserCardPermissionRepository : BaseRepository<UserCardPermission>, IUserCardPermissionRepository
    {
        QPHContext _context;
        public UserCardPermissionRepository(QPHContext context) : base(context) {
            _context = context;
        }

        public async Task RemoveByUserId(int userId)
        {
            var listCardsGrantedId = await _context.UserCardGranted.Where(u => u.id_user == userId).Select(u => u.Id).ToListAsync();
            _entities.RemoveRange(_entities.Where(e => listCardsGrantedId.Contains(e.id_card_granted)));
        }
    }
}