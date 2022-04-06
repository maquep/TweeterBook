using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweeterBook.Contracts.V1;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;
using TweeterBook.Extensions;
using TweeterBook.Services;

namespace TweeterBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class TagsController : Controller
    {
        private readonly IPostService _postService;
        private IMapper _mapper;

        public TagsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the tags
        /// </summary>
        /// <response code="200">Returns all the tags</response>
        [HttpGet(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _postService.GetAllTagsAsync();
            return Ok(_mapper.Map<List<TagResponse>>(tags));
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tagRequest"></param>
        /// <remarks>
        /// Sample **request**:
        /// 
        ///     POST /api/v1/tags
        ///     {
        ///         "name": "Tag name"
        ///     }
        /// </remarks>
        /// <response code="200">Creates new tags</response>
        /// <response code="400">Unable to create the tag due to validation error</response>
        [HttpPost(ApiRoutes.Tags.Create)]
        [ProducesResponseType(typeof(TagResponse), 201)]
        [ProducesResponseType(typeof(TagResponse), 400)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest tagRequest)
        {
            var newTag = new Tag
            {
                Name = tagRequest.Name,
                CreatedOn = System.DateTime.UtcNow,
                CreatorId = HttpContext.GetUserId()
            };

            var created = await _postService.CreateTagAsync(newTag);
            if (created is null)
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create tag" } } });
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + $"/{ApiRoutes.Tags.Get}";

            return Created(locationUri, _mapper.Map<Tag, TagResponse>(newTag));
        }

        [HttpDelete(ApiRoutes.Tags.Delete)]
        //[Authorize(Policy = "MustWorkForMax")]
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            var deleted = await _postService.DeleteTagAsync(tagName);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}