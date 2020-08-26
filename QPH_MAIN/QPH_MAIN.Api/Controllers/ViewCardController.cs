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
    public class ViewCardController : ControllerBase
    {
        private readonly IViewCardService _viewCardService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ViewCardController(IViewCardService cityService, IMapper mapper, IUriService uriService)
        {
            _viewCardService = cityService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve viewCard by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetViewCard(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var viewCard = await _viewCardService.GetViewCard(id);
            var viewCardDto = _mapper.Map<ViewCardDto>(viewCard);
            var response = new ApiResponse<ViewCardDto>(viewCardDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new viewCard
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ViewCardDto viewCardDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var viewCard = _mapper.Map<ViewCard>(viewCardDto);
            await _viewCardService.InsertViewCard(viewCard);
            viewCardDto = _mapper.Map<ViewCardDto>(viewCard);
            var response = new ApiResponse<ViewCardDto>(viewCardDto);
            return Ok(response);
        }

        /// <summary>
        /// Update viewCard
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, ViewCardDto viewCardDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var viewCard = _mapper.Map<ViewCard>(viewCardDto);
            viewCard.Id = id;
            var result = await _viewCardService.UpdateViewCard(viewCard);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove viewCard by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _viewCardService.DeleteViewCard(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}