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
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _roleService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public RolesController(IRolesService roleService, IMapper mapper, IUriService uriService)
        {
            _roleService = roleService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all roles
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RetrieveRoles")]
        public IActionResult GetRoles([FromBody] RolesQueryFilter filters)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var roles = _roleService.GetRoles(filters);
            var rolesDto = _mapper.Map<IEnumerable<RolesDto>>(roles);
            var metadata = new Metadata
            {
                TotalCount = roles.TotalCount,
                PageSize = roles.PageSize,
                CurrentPage = roles.CurrentPage,
                TotalPages = roles.TotalPages,
                HasNextPage = roles.HasNextPage,
                HasPreviousPage = roles.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetRoles))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetRoles))).ToString()
            };
            var response = new ApiResponse<IEnumerable<RolesDto>>(rolesDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve role by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var role = await _roleService.GetRole(id);
            var roleDto = _mapper.Map<RolesDto>(role);
            var response = new ApiResponse<RolesDto>(roleDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new role
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RolesDto roleDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var role = _mapper.Map<Roles>(roleDto);
            await _roleService.InsertRole(role);
            roleDto = _mapper.Map<RolesDto>(role);
            var response = new ApiResponse<RolesDto>(roleDto);
            return Ok(response);
        }

        /// <summary>
        /// Update role
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, RolesDto roleDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var role = _mapper.Map<Roles>(roleDto);
            role.Id = id;
            var result = await _roleService.UpdateRole(role);
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
            var result = await _roleService.DeleteRole(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
