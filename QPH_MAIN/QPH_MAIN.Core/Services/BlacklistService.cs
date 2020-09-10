using Microsoft.Extensions.Options;
using OrderByExtensions;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class BlacklistService : IBlacklistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public BlacklistService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<bool> DeleteBlacklist(int id)
        {
            await _unitOfWork.BlacklistRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Blacklist> GetBlacklist(int id) => await _unitOfWork.BlacklistRepository.GetById(id);

        public  async Task<Blacklist> GetBlacklistByIp(string ip) => await _unitOfWork.BlacklistRepository.GetByIp(ip);

        public PagedList<Blacklist> GetBlacklists(BlacklistQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var blacklists = _unitOfWork.BlacklistRepository.GetAll();
            if (filters.filter != null)
            {
                blacklists = blacklists.Where(x => x.public_ip.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    blacklists = blacklists.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Blacklist>.Create(blacklists, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertBlacklist(Blacklist blacklist)
        {
            await _unitOfWork.BlacklistRepository.Add(blacklist);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBlacklist(Blacklist blacklist)
        {
            var existingBlackList = await _unitOfWork.BlacklistRepository.GetById(blacklist.Id);
            existingBlackList.public_ip = blacklist.public_ip;
            _unitOfWork.BlacklistRepository.Update(existingBlackList);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
