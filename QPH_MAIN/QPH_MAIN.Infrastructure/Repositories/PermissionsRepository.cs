using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class PermissionsRepository : BaseRepository<Permissions>, IPermissionsRepository
    {
        public PermissionsRepository(QPHContext context) : base(context) { }
    }
}