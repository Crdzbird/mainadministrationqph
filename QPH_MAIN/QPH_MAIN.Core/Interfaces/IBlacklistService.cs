using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IBlacklistService
    {
        PagedList<Blacklist> GetBlacklists(BlacklistQueryFilter filters);
        Task<Blacklist> GetBlacklist(int id);
        Task<Blacklist> GetBlacklistByIp(string ip);
        Task InsertBlacklist(Blacklist blacklist);
        Task<bool> UpdateBlacklist(Blacklist blacklist);
        Task<bool> DeleteBlacklist(int id);
    }
}