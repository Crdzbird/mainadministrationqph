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
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public RegionController(IRegionService regionService, IMapper mapper, IUriService uriService)
        {
            _regionService = regionService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all regions
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RetrieveRegions")]
        public IActionResult GetRegions([FromBody] RegionQueryFilter filters)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var regions = _regionService.GetRegions(filters);
            var regionsDto = _mapper.Map<IEnumerable<RegionDto>>(regions);
            var metadata = new Metadata
            {
                TotalCount = regions.TotalCount,
                PageSize = regions.PageSize,
                CurrentPage = regions.CurrentPage,
                TotalPages = regions.TotalPages,
                HasNextPage = regions.HasNextPage,
                HasPreviousPage = regions.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetRegions))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetRegions))).ToString()
            };
            var response = new ApiResponse<IEnumerable<RegionDto>>(regionsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve region by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegion(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var region = await _regionService.GetRegion(id);
            var regionDto = _mapper.Map<RegionDto>(region);
            var response = new ApiResponse<RegionDto>(regionDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new region
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegionDto regionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var region = _mapper.Map<Region>(regionDto);
            await _regionService.InsertRegion(region);
            regionDto = _mapper.Map<RegionDto>(region);
            var response = new ApiResponse<RegionDto>(regionDto);
            return Ok(response);
        }

        /// <summary>
        /// Update region
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, RegionDto regionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var region = _mapper.Map<Region>(regionDto);
            region.Id = id;
            var result = await _regionService.UpdateRegion(region);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove region by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _regionService.DeleteRegion(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}