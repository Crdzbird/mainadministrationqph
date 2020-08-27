using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UserCardGrantedRepository : BaseRepository<UserCardGranted>, IUserCardGrantedRepository
    {
        public UserCardGrantedRepository(QPHContext context) : base(context) { }

        public async Task<int> GetByCardAndUser(int idCard, int idUser)
        {
            return await _entities.Where(e => e.id_card == idCard).Where(e => e.id_user == idUser).Select(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task RemoveByUserId(int userId) => _entities.RemoveRange(await _entities.Where(u => u.id_user == userId).ToListAsync());
        
    }
}