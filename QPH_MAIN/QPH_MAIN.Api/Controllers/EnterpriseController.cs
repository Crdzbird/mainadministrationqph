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
    public class EnterpriseController : ControllerBase
    {
        private readonly IEnterpriseService _enterpriseService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public EnterpriseController(IEnterpriseService enterpriseService, IMapper mapper, IUriService uriService)
        {
            _enterpriseService = enterpriseService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all enterprises
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RetrieveEnterprise")]
        public IActionResult GetEnterprises([FromBody] EnterpriseQueryFilter filters)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var enterprises = _enterpriseService.GetEnterprises(filters);
            var enterpriseDto = _mapper.Map<IEnumerable<EnterpriseDto>>(enterprises);
            var metadata = new Metadata
            {
                TotalCount = enterprises.TotalCount,
                PageSize = enterprises.PageSize,
                CurrentPage = enterprises.CurrentPage,
                TotalPages = enterprises.TotalPages,
                HasNextPage = enterprises.HasNextPage,
                HasPreviousPage = enterprises.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetEnterprises))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetEnterprises))).ToString()
            };
            var response = new ApiResponse<IEnumerable<EnterpriseDto>>(enterpriseDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve enterprise by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnterprise(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var enterprise = await _enterpriseService.GetEnterprise(id);
            var enterpriseDto = _mapper.Map<EnterpriseDto>(enterprise);
            var response = new ApiResponse<EnterpriseDto>(enterpriseDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new enterprise
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnterpriseDto enterpriseDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var enterprise = _mapper.Map<Enterprise>(enterpriseDto);
            await _enterpriseService.InsertEnterprise(enterprise);
            enterpriseDto = _mapper.Map<EnterpriseDto>(enterprise);
            var response = new ApiResponse<EnterpriseDto>(enterpriseDto);
            return Ok(response);
        }

        /// <summary>
        /// Update enterprise
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, EnterpriseDto enterpriseDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var enterprise = _mapper.Map<Enterprise>(enterpriseDto);
            enterprise.Id = id;
            var result = await _enterpriseService.UpdateEnterprise(enterprise);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove enterprise by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _enterpriseService.DeleteEnterprise(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}