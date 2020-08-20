using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserController(IMapper mapper, IUriService uriService, IConfiguration configuration, IUserService userService, ISecurityService securityService, IPasswordService passwordService)
        {
            _mapper = mapper;
            _uriService = uriService;
            _userService = userService;
            _configuration = configuration;
            _passwordService = passwordService;
            _securityService = securityService;
        }

        /// <summary>
        /// Retrieve all users
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetUsers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetUsers([FromQuery] UserQueryFilter filters)
        {
            var users = _userService.GetUsers(filters);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            var metadata = new Metadata
            {
                TotalCount = users.TotalCount,
                PageSize = users.PageSize,
                CurrentPage = users.CurrentPage,
                TotalPages = users.TotalPages,
                HasNextPage = users.HasNextPage,
                HasPreviousPage = users.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetUsers))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetUsers))).ToString()
            };
            var response = new ApiResponse<IEnumerable<UserDto>>(usersDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve user by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            var userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }

        [HttpPost("activateAccount")]
        public async Task<IActionResult> ActivateAccount([FromQuery] string activationCode)
        {
            var result = await _userService.ActivateUserAccount(activationCode);
            return Ok(result);
        }

        /// <summary>
        /// Insert new user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.hashPassword = _passwordService.Hash(user.hashPassword);
            user = await _userService.InsertUser(user);
            var activationUrl = _uriService.GetActivationUri(Url.RouteUrl(nameof(ActivateAccount))).ToString() + $"?activationCode={user.activation_code}";
            System.Diagnostics.Debugger.Break();
            _userService.SendMail(user.activation_code, user.email, activationUrl);
            userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }

        /// <summary>
        /// Update city
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(int id, UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
            var result = await _userService.UpdateUser(user);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove city by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUser(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
