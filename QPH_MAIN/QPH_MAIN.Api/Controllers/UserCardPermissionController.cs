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
    public class UserCardPermissionController : ControllerBase
    {
        private readonly IUserCardPermissionService _userCardPermissionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserCardPermissionController(IUserCardPermissionService cityService, IMapper mapper, IUriService uriService)
        {
            _userCardPermissionService = cityService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve userCardPermission by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCardPermission(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var userCardPermission = await _userCardPermissionService.GetUserCardPermission(id);
            var userCardPermissionDto = _mapper.Map<UserCardPermissionDto>(userCardPermission);
            var response = new ApiResponse<UserCardPermissionDto>(userCardPermissionDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new userCardPermission
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCardPermissionDto userCardPermissionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var userCardPermission = _mapper.Map<UserCardPermission>(userCardPermissionDto);
            await _userCardPermissionService.InsertUserCardPermission(userCardPermission);
            userCardPermissionDto = _mapper.Map<UserCardPermissionDto>(userCardPermission);
            var response = new ApiResponse<UserCardPermissionDto>(userCardPermissionDto);
            return Ok(response);
        }

        /// <summary>
        /// Update userCardPermission
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, UserCardPermissionDto userCardPermissionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var userCardPermission = _mapper.Map<UserCardPermission>(userCardPermissionDto);
            userCardPermission.Id = id;
            var result = await _userCardPermissionService.UpdateUserCardPermission(userCardPermission);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove userCardPermission by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _userCardPermissionService.DeleteUserCardPermission(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}