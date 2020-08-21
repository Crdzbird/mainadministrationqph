using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(QPHContext context) : base(context) { }
        public async Task<IEnumerable<City>> GetCitiesByIdRegion(int regionId) => await _entities.Where(x => x.id_region == regionId).ToListAsync();
    }
}