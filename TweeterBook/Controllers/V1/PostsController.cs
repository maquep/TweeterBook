using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Cache;
using TweeterBook.Contracts.V1;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Requests.Queries;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;
using TweeterBook.Extensions;
using TweeterBook.Helpers;
using TweeterBook.Services;

namespace TweeterBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private IPostService _postServices;
        private IMapper _mapper;
        private readonly IUriService _uriService;

        public PostsController(IPostService postServices, IMapper mapper, IUriService uriService)
        {
            _postServices = postServices;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        [Cache(600)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPostsQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = _mapper.Map<GetAllPostsFilter>(query);

            var posts = await _postServices.GetPostsAsync(filter, paginationFilter);

            var postResonpse = _mapper.Map<List<PostResponse>>(posts);

            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<PostResponse>(postResonpse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginationResponse<PostResponse>(_uriService, paginationFilter, postResonpse);
            return Ok(paginationResponse);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postServices.GetPostByIdAsync(postId);

            if(post == null)
                return NotFound();

            return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Title = postRequest.Title,
                UserId = HttpContext.GetUserId(),
                Tags = postRequest.Tags.Select(t => new PostTag { TagName = t.Name }).ToList(),
            };

            await _postServices.CreatePostAsync(post);

            var locationUri = _uriService.GetPostUri(post.Id.ToString());

            return Created(locationUri, new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
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
                return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
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
