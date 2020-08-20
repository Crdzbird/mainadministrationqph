using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICityService
    {
        PagedList<City> GetCities(CityQueryFilter filters);
        Task<City> GetCity(int id);
        Task InsertCity(City city);
        Task<bool> UpdateCity(City city);
        Task<bool> DeleteCity(int id);
    }
}