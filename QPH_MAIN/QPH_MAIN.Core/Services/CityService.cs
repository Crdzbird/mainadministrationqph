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
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public CityService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<City> GetCity(int id) => await _unitOfWork.CityRepository.GetById(id);

        public PagedList<City> GetCities(CityQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var cities = _unitOfWork.CityRepository.GetAll();
            if(filters.filter != null)
            {
                cities = cities.Where(x => x.name.ToLower().Contains(filters.filter.ToLower()));
            }
            if(filters.RegionId != null)
            {
                cities = cities.Where(x => x.id_region == filters.RegionId);
            }
            if (filters.Name != null)
            {
                cities = cities.Where(x => x.name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    cities = cities.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<City>.Create(cities, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertCity(City city)
        {
            var region = await _unitOfWork.RegionRepository.GetById(city.id_region);
            if (region == null)
            {
                throw new BusinessException("Region doesn't exist");
            }
            await _unitOfWork.CityRepository.Add(city);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCity(City city)
        {
            var existingCity = await _unitOfWork.CityRepository.GetById(city.Id);
            existingCity.name =  city.name;
            existingCity.id_region = city.id_region;
            _unitOfWork.CityRepository.Update(existingCity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCity(int id)
        {
            await _unitOfWork.CityRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}