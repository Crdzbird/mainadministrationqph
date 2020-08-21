using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserViewRepository : IRepository<UserView>
    {
        Task<UserView> GetHierarchyByParent(int parentId);
        Task<UserView> GetHierarchyByChildren(int childrenId);
        Task RemoveByUserId(int userId);
    }
}