using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IViewRepository : IRepository<Views>
    {
        Task<Views> GetViewNameByHierarchyId(int viewId);
    }
}