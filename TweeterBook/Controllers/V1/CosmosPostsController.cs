using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TweeterBook.Contracts.V1;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Domain;
using TweeterBook.Services;

namespace TweeterBook.Controllers.V1
{
    public class CosmosPostsController : Controller
    {
        private readonly ICosmosPostServices _postServices;

        public CosmosPostsController(ICosmosPostServices postServices)
        {
            _postServices = postServices;
        }

        [HttpGet(ApiRoutes.CosmosPosts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postServices.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.CosmosPosts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postServices.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost(ApiRoutes.CosmosPosts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var response = await _postServices.CreatePostAsync(postRequest);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + $"/{ApiRoutes.CosmosPosts.Get}";

            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.CosmosPosts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest updatePostRequest)
        {
            var post = new CosmosPost { Id = postId.ToString(), Title = updatePostRequest.Title };
            var updated = await _postServices.UpdatePostAsync(post);

            if (updated)
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.CosmosPosts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deleted = await _postServices.DeletePostByIdAsync(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
