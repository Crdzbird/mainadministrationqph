using Microsoft.Extensions.Options;
using OrderByExtensions;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class CardsService : ICardsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public CardsService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Cards> GetCard(int id) => await _unitOfWork.CardsRepository.GetById(id);

        public PagedList<Cards> GetCards(CardsQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var cards = _unitOfWork.CardsRepository.GetAll();
            if(filters.filter != null)
            {
                cards = cards.Where(x => x.card.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.Card != null)
            {
                cards = cards.Where(x => x.card.ToLower().Contains(filters.Card.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    cards = cards.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Cards>.Create(cards, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertCard(Cards cards)
        {
            await _unitOfWork.CardsRepository.Add(cards);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCard(Cards cards)
        {
            var existingCard = await _unitOfWork.CardsRepository.GetById(cards.Id);
            existingCard.card = cards.card;
            _unitOfWork.CardsRepository.Update(existingCard);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCard(int id)
        {
            await _unitOfWork.CardsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}