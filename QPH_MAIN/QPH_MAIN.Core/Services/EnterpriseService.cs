using Microsoft.Extensions.Options;
using OrderByExtensions;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public EnterpriseService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Enterprise> GetEnterprise(int id) => await _unitOfWork.EnterpriseRepository.GetById(id);

        public async Task<Enterprise> GetEnterpriseByName(string name) => await _unitOfWork.EnterpriseRepository.GetByName(name);

        public PagedList<Enterprise> GetEnterprises(EnterpriseQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var enterprises = _unitOfWork.EnterpriseRepository.GetAll();
            if (filters.filter != null)
            {
                enterprises = enterprises.Where(x => x.commercial_name.ToLower().Contains(filters.filter.ToLower()));
                enterprises = enterprises.Where(x => x.enterprise_address.ToLower().Contains(filters.enterprise_address.ToLower()));
                enterprises = enterprises.Where(x => x.name_application.ToLower().Contains(filters.name_application.ToLower()));
            }
            if (filters.id_city != null)
            {
                enterprises = enterprises.Where(x => x.id_city == filters.id_city);
            }
            if (filters.name_application != null)
            {
                enterprises = enterprises.Where(x => x.name_application == filters.name_application);
            }    
            if (filters.commercial_name != null)
            {
                enterprises = enterprises.Where(x => x.commercial_name.ToLower().Contains(filters.commercial_name.ToLower()));
            }
            if (filters.enterprise_address != null)
            {
                enterprises = enterprises.Where(x => x.enterprise_address.ToLower().Contains(filters.enterprise_address.ToLower()));
            }
            if (filters.name_application != null)
            {
                enterprises = enterprises.Where(x => x.name_application.ToLower().Contains(filters.name_application.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    enterprises = enterprises.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Enterprise>.Create(enterprises, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertEnterprise(Enterprise enterprise)
        {
            var city = await _unitOfWork.CityRepository.GetById(enterprise.id_city);
            if (city == null)
            {
                throw new BusinessException("City doesn't exist");
            }
            await _unitOfWork.EnterpriseRepository.Add(enterprise);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateEnterprise(Enterprise enterprise)
        {
            var existingEnterprise = await _unitOfWork.EnterpriseRepository.GetById(enterprise.Id);
            existingEnterprise.commercial_name = enterprise.commercial_name;
            existingEnterprise.id_city = enterprise.id_city;
            existingEnterprise.telephone = enterprise.telephone;
            existingEnterprise.email = enterprise.email;
            existingEnterprise.name_application = enterprise.name_application;
            existingEnterprise.enterprise_address = enterprise.enterprise_address;
            existingEnterprise.identification = enterprise.identification;
            existingEnterprise.has_branches = enterprise.has_branches;
            existingEnterprise.latitude = enterprise.latitude;
            existingEnterprise.longitude = enterprise.longitude;
            existingEnterprise.status = enterprise.status;
            _unitOfWork.EnterpriseRepository.Update(existingEnterprise);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEnterprise(int id)
        {
            await _unitOfWork.EnterpriseRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}