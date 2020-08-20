using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRegionService
    {
        PagedList<Region> GetRegions(RegionQueryFilter filters);
        Task<Region> GetRegion(int id);
        Task InsertRegion(Region region);
        Task<bool> UpdateRegion(Region region);
        Task<bool> DeleteRegion(int id);
    }
}