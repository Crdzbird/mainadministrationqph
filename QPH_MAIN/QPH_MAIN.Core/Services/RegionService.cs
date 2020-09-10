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
    public class RegionService : IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public RegionService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Region> GetRegion(int id) => await _unitOfWork.RegionRepository.GetById(id);

        public PagedList<Region> GetRegions(RegionQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var regions = _unitOfWork.RegionRepository.GetAll();
            if (filters.filter != null)
            {
                regions = regions.Where(x => x.name.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.CountryId != null)
            {
                regions = regions.Where(x => x.id_country == filters.CountryId);
            }
            if (filters.Name != null)
            {
                regions = regions.Where(x => x.name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    regions = regions.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Region>.Create(regions, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertRegion(Region region)
        {
            var country = await _unitOfWork.CountryRepository.GetById(region.id_country);
            if (country == null)
            {
                throw new BusinessException("Country doesn't exist");
            }
            await _unitOfWork.RegionRepository.Add(region);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateRegion(Region region)
        {
            var existingRegion = await _unitOfWork.RegionRepository.GetById(region.Id);
            existingRegion.name = region.name;
            existingRegion.id_country = region.id_country;
            _unitOfWork.RegionRepository.Update(existingRegion);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRegion(int id)
        {
            await _unitOfWork.RegionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}