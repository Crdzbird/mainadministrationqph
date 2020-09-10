using QPH_MAIN.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IBlacklistRepository : IRepository<Blacklist>
    {
        Task<Blacklist> GetByIp(string ip);
    }
}
