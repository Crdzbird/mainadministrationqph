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
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistController : ControllerBase
    {
        private readonly IBlacklistService _blacklistService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public BlacklistController(IBlacklistService blacklistService, IMapper mapper, IUriService uriService)
        {
            _blacklistService = blacklistService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all blacklist
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RetrieveBlackLists")]
        public IActionResult GetBlacklists([FromBody] BlacklistQueryFilter filters)
        {
            var blacklists = _blacklistService.GetBlacklists(filters);
            var blacklistsDto = _mapper.Map<IEnumerable<BlacklistDto>>(blacklists);
            var metadata = new Metadata
            {
                TotalCount = blacklists.TotalCount,
                PageSize = blacklists.PageSize,
                CurrentPage = blacklists.CurrentPage,
                TotalPages = blacklists.TotalPages,
                HasNextPage = blacklists.HasNextPage,
                HasPreviousPage = blacklists.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetBlacklists))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetBlacklists))).ToString()
            };
            var response = new ApiResponse<IEnumerable<BlacklistDto>>(blacklistsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve blacklist by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCard(int id)
        {
            //if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var blacklist = await _blacklistService.GetBlacklist(id);
            var blacklistDto = _mapper.Map<BlacklistDto>(blacklist);
            var response = new ApiResponse<BlacklistDto>(blacklistDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new blacklist
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BlacklistDto blacklistDto)
        {
            //if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var blacklist = _mapper.Map<Blacklist>(blacklistDto);
            await _blacklistService.InsertBlacklist(blacklist);
            blacklistDto = _mapper.Map<BlacklistDto>(blacklist);
            var response = new ApiResponse<BlacklistDto>(blacklistDto);
            return Ok(response);
        }

        /// <summary>
        /// Update blacklist
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, BlacklistDto blacklistDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var blacklist = _mapper.Map<Blacklist>(blacklistDto);
            blacklist.Id = id;
            var result = await _blacklistService.UpdateBlacklist(blacklist);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove blacklist by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _blacklistService.DeleteBlacklist(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
