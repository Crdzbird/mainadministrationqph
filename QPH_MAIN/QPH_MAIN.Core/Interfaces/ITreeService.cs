using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ITreeService
    {
        Task<Tree> GetHierarchyTreeByUserId(int userId);
    }
}