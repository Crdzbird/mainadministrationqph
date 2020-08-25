using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByIdCountry(int countryId);
        Task<IEnumerable<User>> GetUsersByIdRole(int roleId);
        Task<IEnumerable<User>> GetUsersByIdEnterprise(int enterpriseId);
        Task<User> GetUserByActivationCode(string activationCode);
        Task<User> GetDetailUser(int userId);
        Task<User> GetByUsername(string username);
    }
}