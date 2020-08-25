using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ICardsService
    {
        PagedList<Cards> GetCards(CardsQueryFilter filters);
        Task<Cards> GetCard(int id);
        Task InsertCard(Cards cards);
        Task<bool> UpdateCard(Cards cards);
        Task<bool> DeleteCard(int id);
    }
}