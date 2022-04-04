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
    public class TagsController : Controller
    {
        private readonly IPostService _postService;
        private IMapper _mapper;

        public TagsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _postService.GetAllTagsAsync();
            return Ok(_mapper.Map<List<TagResponse>>(tags));
        }

        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest tagRequest)
        {
            var newTag = new Tag
            {
                Name = tagRequest.Name,
                CreatedOn = System.DateTime.UtcNow,
                CreatorId = HttpContext.GetUserId()
            };

            await _postService.CreateTagAsync(newTag);

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