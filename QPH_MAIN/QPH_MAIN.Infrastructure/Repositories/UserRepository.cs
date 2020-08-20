using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(QPHContext context) : base(context) { }

        public async Task<User> GetUserByActivationCode(string activationCode)
        {
            return await _entities.SingleOrDefaultAsync(x => x.activation_code == activationCode);
        }

        public async Task<IEnumerable<User>> GetUsersByIdCountry(int countryId)
        {
            return await _entities.Where(x => x.id_country == countryId).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByIdEnterprise(int enterpriseId)
        {
            return await _entities.Where(x => x.id_enterprise == enterpriseId).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByIdRole(int roleId)
        {
            return await _entities.Where(x => x.id_role == roleId).ToListAsync();
        }

    }
}
