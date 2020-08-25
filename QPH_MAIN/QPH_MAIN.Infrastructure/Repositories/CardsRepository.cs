using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class CardsRepository : BaseRepository<Cards>, ICardsRepository
    {
        public CardsRepository(QPHContext context) : base(context) { }
    }
}