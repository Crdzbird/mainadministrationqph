using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserCardPermissionService
    {
        Task<UserCardPermission> GetUserCardPermission(int id);
        Task InsertUserCardPermission(UserCardPermission userCardPermission);
        Task<bool> UpdateUserCardPermission(UserCardPermission userCardPermission);
        Task<bool> DeleteUserCardPermission(int id);
        Task DeletePermissionByUserId(int userId);
    }
}