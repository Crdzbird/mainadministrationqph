using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRolesService
    {
        PagedList<Roles> GetRoles(RolesQueryFilter filters);
        Task<Roles> GetRole(int id);
        Task<Roles> GetRoleByName(string name);
        Task InsertRole(Roles role);
        Task<bool> UpdateRole(Roles role);
        Task<bool> DeleteRole(int id);
    }
}