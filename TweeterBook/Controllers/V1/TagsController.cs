using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TweeterBook.Contracts.V1;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Domain;
using TweeterBook.Extensions;
using TweeterBook.Services;

namespace TweeterBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        private readonly IPostService _postService;

        public TagsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllTagsAsync());
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

            var response = await _postService.CreateTagAsync(newTag);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + $"/{ApiRoutes.Tags.Get}";

            return Created(locationUri, response);
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
