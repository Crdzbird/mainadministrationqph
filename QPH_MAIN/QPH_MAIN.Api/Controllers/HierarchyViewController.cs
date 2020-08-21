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
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HierarchyViewController : ControllerBase
    {
        private readonly IViewService _viewService;
        private readonly IUserViewService _hierarchyViewService;
        private readonly ITreeService _treeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public HierarchyViewController(ITreeService treeService, IViewService viewService, IUserViewService hierarchyViewService, IMapper mapper, IUriService uriService)
        {
            _treeService = treeService;
            _viewService = viewService;
            _hierarchyViewService = hierarchyViewService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all views
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetViews))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ViewsDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetViews([FromQuery]ViewQueryFilter filters)
        {
            var views = _viewService.GetViews(filters);
            var viewsDto = _mapper.Map<IEnumerable<ViewsDto>>(views);
            var metadata = new Metadata
            {
                TotalCount = views.TotalCount,
                PageSize = views.PageSize,
                CurrentPage = views.CurrentPage,
                TotalPages = views.TotalPages,
                HasNextPage = views.HasNextPage,
                HasPreviousPage = views.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetViews))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetViews))).ToString()
            };
            var response = new ApiResponse<IEnumerable<ViewsDto>>(viewsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve view by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetView(int id)
        {
            var views = await _viewService.GetView(id);
            var ViewsDto = _mapper.Map<ViewsDto>(views);
            var response = new ApiResponse<ViewsDto>(ViewsDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new view
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ViewsDto ViewsDto)
        {
            var view = _mapper.Map<Views>(ViewsDto);
            await _viewService.InsertView(view);
            ViewsDto = _mapper.Map<ViewsDto>(view);
            var response = new ApiResponse<ViewsDto>(ViewsDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new hierarchy by user and view
        /// </summary>
        [HttpPost("rebuildHierarchy")]
        public async Task<IActionResult> Post([FromBody] TreeDto treeDto)
        {
            var tree = _mapper.Map<Tree>(treeDto);
            await _viewService.DeleteHierarchyByUserId(1);
            await _viewService.RebuildHierarchy(tree, 1);

            return Ok(null);
        }

        /// <summary>
        /// Update view
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(int id, ViewsDto ViewsDto)
        {
            var view = _mapper.Map<Views>(ViewsDto);
            view.Id = id;
            var result = await _viewService.UpdateView(view);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Build HierarchyView By User Authenticated.
        /// </summary>
        [Authorize]
        [HttpGet("buildHierarchyViewByUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObtainTree()
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            string userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            System.Diagnostics.Debugger.Break();
            return Ok(await _treeService.GetHierarchyTreeByUserId(int.Parse(userId)));
        }

        /// <summary>
        /// Remove view by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _viewService.DeleteView(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}