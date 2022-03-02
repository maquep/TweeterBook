using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TweeterBook.Contracts.V1;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;
using TweeterBook.Services;

namespace TweeterBook.Controllers.V1
{
    public class PostsController : Controller
    {
        private IPostServices _postServices;

        public PostsController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postServices.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _postServices.GetPostById(postId);

            if(post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] PostRequest postRequest)
        {
            var response = _postServices.CreatePost(postRequest);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + $"/{ApiRoutes.Posts.Get}";

            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest updatePostRequest)
        {
            var post = new Post {Id = postId, Title = updatePostRequest.Title };
            var updated = _postServices.UpdatePost(post);

            if(updated)
                return Ok(post);

            return NotFound();
        }
    }
}
