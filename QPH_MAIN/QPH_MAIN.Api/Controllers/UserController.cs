﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserController(IMapper mapper, IUriService uriService, IConfiguration configuration, IUserService userService, IPasswordService passwordService)
        {
            _mapper = mapper;
            _uriService = uriService;
            _userService = userService;
            _configuration = configuration;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Retrieve all users
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost("RetrieveUsers")]
        public IActionResult GetUsers([FromBody] UserQueryFilter filters)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
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
        /// Obtain UserDetail by AuthenticationToken
        /// </summary>
        [Authorize]
        [HttpGet("detailedUser")]
        public async Task<IActionResult> DetailedUser()
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            string userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            return Ok(await _userService.GetUserDetail(int.Parse(userId)));
        }

        /// <summary>
        /// Login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }
            return NotFound("User not activated or not found");
        }


        /// <summary>
        /// Retrieve user by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var user = await _userService.GetUser(id);
            var userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }

        /// <summary>
        /// Activate user by activationCode
        /// </summary>
        [HttpGet("activateAccount")]
        public async Task<IActionResult> ActivateAccount([FromQuery] string activationCode)
        {
            if (await _userService.ActivateUserAccount(activationCode)) return Ok("Account Activated");
            return NotFound();
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
            var activationUrl = _uriService.GetActivationUri(Url.RouteUrl(nameof(ActivateAccount))).ToString() + $"api/User/activateAccount?activationCode={user.activation_code}";
            _userService.SendMail(user.activation_code, user.email, activationUrl);
            userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }

        /// <summary>
        /// Update user
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, UserDto userDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
            var result = await _userService.UpdateUser(user);
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
            var result = await _userService.DeleteUser(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        private async Task<(bool, User)> IsValidUser(UserLogin login)
        {
            var user = await _userService.GetLoginByCredentials(login);
            if (user == null) return (false, null);
            var isValid = _passwordService.Check(user.hashPassword, login.Password);
            return (isValid, user);
        }

        private string GenerateToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.nickname),
                new Claim("User", user.email),
                new Claim("Id", user.Id.ToString()),
            };
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(30)
            );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}