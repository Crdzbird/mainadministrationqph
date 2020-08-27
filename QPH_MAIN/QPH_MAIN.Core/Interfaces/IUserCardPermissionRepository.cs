using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserCardPermissionRepository : IRepository<UserCardPermission> {
        Task RemoveByUserId(int userId);
    }
}