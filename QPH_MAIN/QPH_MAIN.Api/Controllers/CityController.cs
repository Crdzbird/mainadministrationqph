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
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CityController(ICityService cityService, IMapper mapper, IUriService uriService)
        {
            _cityService = cityService;
            _mapper = mapper;
            _uriService = uriService;
        }


        /// <summary>
        /// Retrieve all cities
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RetrieveCities")]
        public IActionResult GetCities([FromBody] CityQueryFilter filters)
        {
            var cities = _cityService.GetCities(filters);
            var citiesDto = _mapper.Map<IEnumerable<CityDto>>(cities);
            var metadata = new Metadata
            {
                TotalCount = cities.TotalCount,
                PageSize = cities.PageSize,
                CurrentPage = cities.CurrentPage,
                TotalPages = cities.TotalPages,
                HasNextPage = cities.HasNextPage,
                HasPreviousPage = cities.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetCities))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetCities))).ToString()
            };
            var response = new ApiResponse<IEnumerable<CityDto>>(citiesDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve city by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var city = await _cityService.GetCity(id);
            var cityDto = _mapper.Map<CityDto>(city);
            var response = new ApiResponse<CityDto>(cityDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new city
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CityDto cityDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var city = _mapper.Map<City>(cityDto);
            await _cityService.InsertCity(city);
            cityDto = _mapper.Map<CityDto>(city);
            var response = new ApiResponse<CityDto>(cityDto);
            return Ok(response);
        }

        /// <summary>
        /// Update city
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, CityDto cityDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var city = _mapper.Map<City>(cityDto);
            city.Id = id;
            var result = await _cityService.UpdateCity(city);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove city by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _cityService.DeleteCity(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}