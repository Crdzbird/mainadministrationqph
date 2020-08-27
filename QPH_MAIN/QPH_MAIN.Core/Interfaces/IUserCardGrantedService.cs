using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserCardGrantedService
    {
        Task<UserCardGranted> GetUserCardGranted(int id);
        Task InsertUserCardGranted(UserCardGranted userCardGranted);
        Task<bool> UpdateUserCardGranted(UserCardGranted userCardGranted);
        Task<bool> DeleteUserCardGranted(int id);
        Task DeleteUserCardGrantedByUserId(int userId);
    }
}