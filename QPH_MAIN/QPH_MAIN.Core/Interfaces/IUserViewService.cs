using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserViewService
    {
        Task<UserView> GetHierarchy(int id);
        Task InsertHierarchy(UserView hierarchyView);
        Task<bool> UpdateHierarchyView(UserView hierarchyView);
        Task<bool> DeleteHierarchyView(int id);
        Task RemoveByUserId(int userId);
    }
}