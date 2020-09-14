using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UnitOfWorkChannel : IUnitOfWorkChannel
    {
        private readonly QPHChannelContext _context;
        private readonly IChannelRepository _channelRepository;

        public UnitOfWorkChannel(QPHChannelContext context)
        {
            _context = context;
        }
        public IChannelRepository ChannelRepository => _channelRepository ?? new ChannelRepository(_context);
        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}