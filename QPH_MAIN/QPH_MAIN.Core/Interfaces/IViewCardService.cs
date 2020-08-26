using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IViewCardService
    {
        Task<ViewCard> GetViewCard(int id);
        Task InsertViewCard(ViewCard viewCard);
        Task<bool> UpdateViewCard(ViewCard viewCard);
        Task<bool> DeleteViewCard(int id);
    }
}