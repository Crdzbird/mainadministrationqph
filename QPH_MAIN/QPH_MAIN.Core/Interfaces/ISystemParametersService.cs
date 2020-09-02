using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ISystemParametersService
    {
        Task<SystemParameters> GetSystemParameters(string code);
        Task InsertSystemParameters(SystemParameters systemParameters);
        Task<bool> UpdateSystemParameters(SystemParameters systemParameters);
        Task<bool> DeleteSystemParameters(string code);
    }
}