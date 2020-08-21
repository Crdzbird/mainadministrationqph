﻿using AutoMapper;
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
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CountryController(ICountryService countryService, IMapper mapper, IUriService uriService)
        {
            _countryService = countryService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all countries
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetCountries))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CountryDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCountries([FromQuery]CountryQueryFilter filters)
        {
            var countries = _countryService.GetCountries(filters);
            var countriesDto = _mapper.Map<IEnumerable<CountryDto>>(countries);
            var metadata = new Metadata
            {
                TotalCount = countries.TotalCount,
                PageSize = countries.PageSize,
                CurrentPage = countries.CurrentPage,
                TotalPages = countries.TotalPages,
                HasNextPage = countries.HasNextPage,
                HasPreviousPage = countries.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetCountries))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetCountries))).ToString()
            };
            var response = new ApiResponse<IEnumerable<CountryDto>>(countriesDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve country by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _countryService.GetCountry(id);
            var countryDto = _mapper.Map<CountryDto>(country);
            var response = new ApiResponse<CountryDto>(countryDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new country
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountryDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
            await _countryService.InsertCountry(country);
            countryDto = _mapper.Map<CountryDto>(country);
            var response = new ApiResponse<CountryDto>(countryDto);
            return Ok(response);
        }

        /// <summary>
        /// Update country
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(int id, CountryDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
            country.Id = id;
            var result = await _countryService.UpdateCountry(country);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove country by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _countryService.DeleteCountry(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}