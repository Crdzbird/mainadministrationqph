using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}