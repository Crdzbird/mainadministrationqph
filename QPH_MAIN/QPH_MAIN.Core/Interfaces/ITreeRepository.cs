using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ITreeRepository : IRepository<Tree>
    {
        Task<Tree> GetTreeByUserId(int userId);
    }
}