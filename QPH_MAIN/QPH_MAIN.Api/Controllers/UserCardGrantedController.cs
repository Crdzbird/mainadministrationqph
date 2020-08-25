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
    public class UserCardGrantedController : ControllerBase
    {
        private readonly IUserCardGrantedService _userCardGrantedService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserCardGrantedController(IUserCardGrantedService cityService, IMapper mapper, IUriService uriService)
        {
            _userCardGrantedService = cityService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve userCardGranted by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCardGranted(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var userCardGranted = await _userCardGrantedService.GetUserCardGranted(id);
            var userCardGrantedDto = _mapper.Map<UserCardGrantedDto>(userCardGranted);
            var response = new ApiResponse<UserCardGrantedDto>(userCardGrantedDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new userCardGranted
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCardGrantedDto userCardGrantedDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var userCardGranted = _mapper.Map<UserCardGranted>(userCardGrantedDto);
            await _userCardGrantedService.InsertUserCardGranted(userCardGranted);
            userCardGrantedDto = _mapper.Map<UserCardGrantedDto>(userCardGranted);
            var response = new ApiResponse<UserCardGrantedDto>(userCardGrantedDto);
            return Ok(response);
        }

        /// <summary>
        /// Update userCardGranted
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, UserCardGrantedDto userCardGrantedDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var userCardGranted = _mapper.Map<UserCardGranted>(userCardGrantedDto);
            userCardGranted.Id = id;
            var result = await _userCardGrantedService.UpdateUserCardGranted(userCardGranted);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove userCardGranted by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _userCardGrantedService.DeleteUserCardGranted(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}