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
    public class PermissionsService : IPermissionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PermissionsService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Permissions> GetPermission(int id) => await _unitOfWork.PermissionsRepository.GetById(id);

        public PagedList<Permissions> GetPermissions(PermissionsQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var permissions = _unitOfWork.PermissionsRepository.GetAll();
            if(filters.filter != null)
            {
                permissions = permissions.Where(x => x.permission.ToLower().Contains(filters.filter.ToLower()));
            }
            if(filters.Name != null)
            {
                permissions = permissions.Where(x => x.permission == filters.Name);
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    permissions = permissions.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Permissions>.Create(permissions, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertPermission(Permissions permissions)
        {
            await _unitOfWork.PermissionsRepository.Add(permissions);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePermission(Permissions permissions)
        {
            var existingPermission = await _unitOfWork.PermissionsRepository.GetById(permissions.Id);
            existingPermission.permission = permissions.permission;
            _unitOfWork.PermissionsRepository.Update(existingPermission);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePermission(int id)
        {
            await _unitOfWork.PermissionsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}