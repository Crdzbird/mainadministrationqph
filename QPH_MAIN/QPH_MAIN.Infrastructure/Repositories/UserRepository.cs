using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(QPHContext context) : base(context) { }

        public async Task<bool> CheckDuplicatedEmail(string email)
        {
            var result = await _entities.FirstOrDefaultAsync(u => u.email == email);
            return (result != null) ? true : false;
        }

        public async Task<bool> CheckDuplicatedNickname(string nickname)
        {
            var result = await _entities.FirstOrDefaultAsync(u => u.nickname == nickname);
            return (result != null) ? true : false;
        }

        public async Task<bool> CheckDuplicatedPhone(string phone)
        {
            var result = await _entities.Where(u => u.phone_number == phone).Where(u => u.phone_number.ToLower() != "N/A".ToLower()).FirstOrDefaultAsync();
            return (result != null) ? true : false;
        }

        public async Task<User> GetByUsername(string username) => await _entities.Where(x => x.email == username).Where(x => x.status == true).Where(x => x.is_account_activated == true).FirstOrDefaultAsync();
        public async Task<User> GetDetailUser(int userId) => await _entities.Include(r => r.roles).Include(e => e.enterprise).Include(c => c.country).Where(u => u.Id == userId).FirstOrDefaultAsync();
        public async Task<User> GetUserByActivationCode(string activationCode) => await _entities.Where(x => x.activation_code == activationCode && x.activation_code != "" && x.is_account_activated == false && x.status == false).FirstOrDefaultAsync();
        public async Task<IEnumerable<User>> GetUsersByIdCountry(int countryId) => await _entities.Where(x => x.id_country == countryId).ToListAsync();
        public async Task<IEnumerable<User>> GetUsersByIdEnterprise(int enterpriseId) => await _entities.Where(x => x.id_enterprise == enterpriseId).ToListAsync();
        public async Task<IEnumerable<User>> GetUsersByIdRole(int roleId) => await _entities.Where(x => x.id_role == roleId).ToListAsync();
    }
}