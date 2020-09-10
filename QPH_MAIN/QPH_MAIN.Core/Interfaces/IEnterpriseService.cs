using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IEnterpriseService
    {
        PagedList<Enterprise> GetEnterprises(EnterpriseQueryFilter filters);
        Task<Enterprise> GetEnterprise(int id);
        Task<Enterprise> GetEnterpriseByName(string name);
        Task InsertEnterprise(Enterprise enterprise);
        Task<bool> UpdateEnterprise(Enterprise enterprise);
        Task<bool> DeleteEnterprise(int id);
    }
}