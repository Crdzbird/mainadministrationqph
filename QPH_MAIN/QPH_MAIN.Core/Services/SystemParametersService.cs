using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class SystemParametersService : ISystemParametersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemParametersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SystemParameters> GetSystemParameters(string code) => await _unitOfWork.SystemParametersRepository.GetByCode(code);

        public async Task InsertSystemParameters(SystemParameters systemParameters)
        {
            await _unitOfWork.SystemParametersRepository.Add(systemParameters);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateSystemParameters(SystemParameters systemParameters)
        {
            var existingSystemParameters = await _unitOfWork.SystemParametersRepository.GetByCode(systemParameters.Code);
            existingSystemParameters.description =  systemParameters.description;
            existingSystemParameters.value = systemParameters.value;
            existingSystemParameters.status = systemParameters.status;
            _unitOfWork.SystemParametersRepository.Update(existingSystemParameters);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSystemParameters(string code)
        {
            await _unitOfWork.SystemParametersRepository.Delete(code);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}