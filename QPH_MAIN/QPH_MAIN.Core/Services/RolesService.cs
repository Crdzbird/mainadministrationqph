using Microsoft.Extensions.Options;
using OrderByExtensions;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public RolesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Roles> GetRole(int id) => await _unitOfWork.RolesRepository.GetById(id);

        public async Task<Roles> GetRoleByName(string name) => await _unitOfWork.RolesRepository.GetByName(name);

        public PagedList<Roles> GetRoles(RolesQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var roles = _unitOfWork.RolesRepository.GetAll();
            if (filters.filter != null)
            {
                roles = roles.Where(x => x.rolename.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.rolename != null)
            {
                roles = roles.Where(x => x.rolename == filters.rolename);
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    roles = roles.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Roles>.Create(roles, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertRole(Roles role)
        {
            await _unitOfWork.RolesRepository.Add(role);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateRole(Roles role)
        {
            var existingRole = await _unitOfWork.RolesRepository.GetById(role.Id);
            existingRole.rolename = role.rolename;
            existingRole.status = role.status;
            _unitOfWork.RolesRepository.Update(existingRole);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRole(int id)
        {
            await _unitOfWork.RolesRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}