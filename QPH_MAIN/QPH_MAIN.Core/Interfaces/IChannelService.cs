using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IChannelService
    {
        PagedList<Channel> GetChannels(ChannelQueryFilter filters);
        Task<Channel> GetChannel(int id);
        Task InsertChannel(Channel channel);
        Task<bool> UpdateChannel(Channel channel);
        Task<bool> DeleteChannel(int id);
    }
}