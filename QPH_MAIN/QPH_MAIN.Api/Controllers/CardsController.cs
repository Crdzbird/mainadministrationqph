using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QPH_MAIN.Api.Responses;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService _cardsService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CardsController(ICardsService cityService, IMapper mapper, IUriService uriService)
        {
            _cardsService = cityService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all cards
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RetrieveCards")]
        public IActionResult GetCards([FromBody] CardsQueryFilter filters)
        {
            var cards = _cardsService.GetCards(filters);
            var citiesDto = _mapper.Map<IEnumerable<CardsDto>>(cards);
            var metadata = new Metadata
            {
                TotalCount = cards.TotalCount,
                PageSize = cards.PageSize,
                CurrentPage = cards.CurrentPage,
                TotalPages = cards.TotalPages,
                HasNextPage = cards.HasNextPage,
                HasPreviousPage = cards.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetCards))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetCards))).ToString()
            };
            var response = new ApiResponse<IEnumerable<CardsDto>>(citiesDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve card by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCard(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var card = await _cardsService.GetCard(id);
            var cardDto = _mapper.Map<CardsDto>(card);
            var response = new ApiResponse<CardsDto>(cardDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new card
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CardsDto cardDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var card = _mapper.Map<Cards>(cardDto);
            await _cardsService.InsertCard(card);
            cardDto = _mapper.Map<CardsDto>(card);
            var response = new ApiResponse<CardsDto>(cardDto);
            return Ok(response);
        }

        /// <summary>
        /// Update card
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, CardsDto cardDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var card = _mapper.Map<Cards>(cardDto);
            card.Id = id;
            var result = await _cardsService.UpdateCard(card);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove card by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _cardsService.DeleteCard(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}