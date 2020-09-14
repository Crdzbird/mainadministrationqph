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

namespace QPH_MAIN.Channel.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ChannelController(IChannelService channelService, IMapper mapper, IUriService uriService)
        {
            _channelService = channelService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all channels
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Retrievechannels")]
        public IActionResult Getchannels([FromBody] ChannelQueryFilter filters)
        {
            var channels = _channelService.GetChannels(filters);
            var channelsDto = _mapper.Map<IEnumerable<ChannelDto>>(channels);
            var metadata = new Metadata
            {
                TotalCount = channels.TotalCount,
                PageSize = channels.PageSize,
                CurrentPage = channels.CurrentPage,
                TotalPages = channels.TotalPages,
                HasNextPage = channels.HasNextPage,
                HasPreviousPage = channels.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(Getchannels))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(Getchannels))).ToString()
            };
            var response = new ApiResponse<IEnumerable<ChannelDto>>(channelsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve Channel by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannel(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var channel = await _channelService.GetChannel(id);
            var channelDto = _mapper.Map<ChannelDto>(channel);
            var response = new ApiResponse<ChannelDto>(channelDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new channel
        /// </summary>
        /*[Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChannelDto channelDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var channel = _mapper.Map<Channel>(channelDto);
            await _channelService.InsertChannel(channel);
            channelDto = _mapper.Map<ChannelDto>(channel);
            var response = new ApiResponse<ChannelDto>(channelDto);
            return Ok(response);
        }*/

        /// <summary>
        /// Update channel
        /// </summary>
        /*[Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, ChannelDto channelDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var channel = _mapper.Map<Channel>(channelDto);
            channel.Id = id;
            var result = await _channelService.UpdateChannel(channel);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }*/

        /// <summary>
        /// Remove card by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _channelService.DeleteChannel(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}