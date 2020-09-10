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
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public CountryService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Country> GetCountry(int id) => await _unitOfWork.CountryRepository.GetById(id);

        public async Task<Country> GetCountryByName(string name) => await _unitOfWork.CountryRepository.GetByName(name);

        public PagedList<Country> GetCountries(CountryQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var countries = _unitOfWork.CountryRepository.GetAll();
            if (filters.filter != null)
            {
                countries = countries.Where(x => x.name.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.Name != null)
            {
                countries = countries.Where(x => x.name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    countries = countries.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Country>.Create(countries, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertCountry(Country country)
        {
            await _unitOfWork.CountryRepository.Add(country);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCountry(Country country)
        {
            var existingCountry = await _unitOfWork.CountryRepository.GetById(country.Id);
            existingCountry.name =  country.name;
            _unitOfWork.CountryRepository.Update(existingCountry);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCountry(int id)
        {
            await _unitOfWork.CountryRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}