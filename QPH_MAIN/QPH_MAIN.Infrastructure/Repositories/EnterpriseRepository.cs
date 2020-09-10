using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class EnterpriseRepository : BaseRepository<Enterprise>, IEnterpriseRepository
    {
        public EnterpriseRepository(QPHContext context) : base(context) { }
        public async Task<Enterprise> GetByName(string name) => await _entities.FirstOrDefaultAsync(x => x.commercial_name.ToLower() == name.ToLower());

    }
}