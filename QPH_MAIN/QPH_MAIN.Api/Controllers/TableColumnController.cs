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
    public class TableColumnController : ControllerBase
    {
        private readonly ITableColumnService _tableColumnService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TableColumnController(ITableColumnService tableColumnService, IMapper mapper, IUriService uriService)
        {
            _tableColumnService = tableColumnService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve table Detail by TableName
        /// </summary>
        [Authorize]
        [HttpGet("{tablename}")]
        public async Task<IActionResult> GetTableColumnDetail(string tablename)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var tableColumnService = await _tableColumnService.GetByTableColumnWithSchema(tablename, "dbo");
            return Ok(tableColumnService);
        }
    }
}