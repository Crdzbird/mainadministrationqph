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
    public class SystemParametersController : ControllerBase
    {
        private readonly ISystemParametersService _systemParametersService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public SystemParametersController(ISystemParametersService systemParametersService, IMapper mapper, IUriService uriService)
        {
            _systemParametersService = systemParametersService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve systemParametersService by id
        /// </summary>
        [Authorize]
        [HttpGet("{code}")]
        public async Task<IActionResult> GetSystemParametersService(string code)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var systemParametersService = await _systemParametersService.GetSystemParameters(code);
            var systemParametersServiceDto = _mapper.Map<SystemParametersDto>(systemParametersService);
            var response = new ApiResponse<SystemParametersDto>(systemParametersServiceDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new systemParametersService
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SystemParametersDto systemParametersServiceDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var systemParametersService = _mapper.Map<SystemParameters>(systemParametersServiceDto);
            await _systemParametersService.InsertSystemParameters(systemParametersService);
            systemParametersServiceDto = _mapper.Map<SystemParametersDto>(systemParametersService);
            var response = new ApiResponse<SystemParametersDto>(systemParametersServiceDto);
            return Ok(response);
        }

        /// <summary>
        /// Update systemParametersService
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(string code, SystemParametersDto systemParametersServiceDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var systemParametersService = _mapper.Map<SystemParameters>(systemParametersServiceDto);
            systemParametersService.Code = code;
            var result = await _systemParametersService.UpdateSystemParameters(systemParametersService);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove systemParametersService by id
        /// </summary>
        [Authorize]
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _systemParametersService.DeleteSystemParameters(code);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}