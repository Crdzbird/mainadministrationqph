using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}