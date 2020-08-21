using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(QPHContext context) : base(context) { }
        public async Task<IEnumerable<Region>> GetRegionsByIdCountry(int countryId) => await _entities.Where(x => x.id_country == countryId).ToListAsync();
    }
}