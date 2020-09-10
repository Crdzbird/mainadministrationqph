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
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionsService _permissionsService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PermissionsController(IPermissionsService permissionsService, IMapper mapper, IUriService uriService)
        {
            _permissionsService = permissionsService;
            _mapper = mapper;
            _uriService = uriService;
        }


        /// <summary>
        /// Retrieve all permissions
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RetrievePermissions")]
        public IActionResult GetPermissions([FromBody] PermissionsQueryFilter filters)
        {
            var Permissions = _permissionsService.GetPermissions(filters);
            var permissionsDto = _mapper.Map<IEnumerable<PermissionsDto>>(Permissions);
            var metadata = new Metadata
            {
                TotalCount = Permissions.TotalCount,
                PageSize = Permissions.PageSize,
                CurrentPage = Permissions.CurrentPage,
                TotalPages = Permissions.TotalPages,
                HasNextPage = Permissions.HasNextPage,
                HasPreviousPage = Permissions.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPermissions))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPermissions))).ToString()
            };
            var response = new ApiResponse<IEnumerable<PermissionsDto>>(permissionsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve permission by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermission(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var permission = await _permissionsService.GetPermission(id);
            var permissionDto = _mapper.Map<PermissionsDto>(permission);
            var response = new ApiResponse<PermissionsDto>(permissionDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new permission
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PermissionsDto permissionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var permission = _mapper.Map<Permissions>(permissionDto);
            await _permissionsService.InsertPermission(permission);
            permissionDto = _mapper.Map<PermissionsDto>(permission);
            var response = new ApiResponse<PermissionsDto>(permissionDto);
            return Ok(response);
        }

        /// <summary>
        /// Update permission
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, PermissionsDto permissionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var permission = _mapper.Map<Permissions>(permissionDto);
            permission.Id = id;
            var result = await _permissionsService.UpdatePermission(permission);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove permission by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _permissionsService.DeletePermission(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}