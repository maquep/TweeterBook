using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PostsController : Controller
    {
        private IPostService _postServices;
        private IMapper _mapper;

        public PostsController(IPostService postServices, IMapper mapper)
        {
            _postServices = postServices;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postServices.GetPostsAsync();
            return Ok(_mapper.Map<List<PostResponse>>(posts));
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postServices.GetPostByIdAsync(postId);

            if(post == null)
                return NotFound();

            return Ok(_mapper.Map<PostResponse>(post));
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Title = postRequest.Title,
                UserId = HttpContext.GetUserId()
            };

            await _postServices.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + $"/{ApiRoutes.Posts.Get}";

            return Created(locationUri, _mapper.Map<PostResponse>(post));
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest updatePostRequest)
        {
            var userOwnsPost = await _postServices.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do now have access to this post" });
            }

            var post = await _postServices.GetPostByIdAsync(postId);
            post.Title = updatePostRequest.Title;
            var updated = await _postServices.UpdatePostAsync(post);

            if (updated)
            {
                return Ok(_mapper.Map<PostResponse>(post));
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postServices.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do now have access to this post" });
            }

            var deleted = await _postServices.DeletePostByIdAsync(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
