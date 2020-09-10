using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICountryRepository : IRepository<Country> {
        Task<Country> GetByName(string name);
    }
}