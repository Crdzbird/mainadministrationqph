using System;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUnitOfWorkChannel : IDisposable
    {
        IChannelRepository ChannelRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}