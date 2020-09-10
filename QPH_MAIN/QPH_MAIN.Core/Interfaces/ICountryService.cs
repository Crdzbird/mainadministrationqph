using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICountryService
    {
        PagedList<Country> GetCountries(CountryQueryFilter filters);
        Task<Country> GetCountry(int id);
        Task<Country> GetCountryByName(string name);
        Task InsertCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> DeleteCountry(int id);
    }
}