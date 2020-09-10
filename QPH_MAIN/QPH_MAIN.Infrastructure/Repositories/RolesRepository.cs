using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        public RolesRepository(QPHContext context) : base(context) { }

        public async Task<Roles> GetByName(string name) => await _entities.FirstOrDefaultAsync(x => x.rolename.ToLower() == name.ToLower());
    }
}