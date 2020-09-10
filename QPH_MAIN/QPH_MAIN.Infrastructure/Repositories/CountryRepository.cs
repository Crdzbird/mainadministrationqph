using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(QPHContext context) : base(context) { }
        public async Task<Country> GetByName(string name) => await _entities.FirstOrDefaultAsync(x => x.name.ToLower() == name.ToLower());
    }
}
