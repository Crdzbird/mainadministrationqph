using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class ViewCardService : IViewCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public ViewCardService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<ViewCard> GetViewCard(int id) => await _unitOfWork.ViewCardRepository.GetById(id);

        public async Task InsertViewCard(ViewCard viewCards)
        {
            await _unitOfWork.ViewCardRepository.Add(viewCards);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateViewCard(ViewCard viewCards)
        {
            var existingCard = await _unitOfWork.ViewCardRepository.GetById(viewCards.Id);
            existingCard.card = viewCards.card;
            _unitOfWork.ViewCardRepository.Update(existingCard);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteViewCard(int id)
        {
            await _unitOfWork.ViewCardRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}