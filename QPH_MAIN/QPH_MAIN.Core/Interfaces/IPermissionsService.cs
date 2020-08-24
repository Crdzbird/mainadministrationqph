using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IPermissionsService
    {
        PagedList<Permissions> GetPermissions(PermissionsQueryFilter filters);
        Task<Permissions> GetPermission(int id);
        Task InsertPermission(Permissions permissions);
        Task<bool> UpdatePermission(Permissions permissions);
        Task<bool> DeletePermission(int id);
    }
}