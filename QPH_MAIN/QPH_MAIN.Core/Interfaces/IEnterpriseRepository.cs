using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IEnterpriseRepository : IRepository<Enterprise> {
        Task<Enterprise> GetByName(string name);

    }
}