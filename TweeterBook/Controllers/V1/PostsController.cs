using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TweeterBook.Contracts.V1;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;

namespace TweeterBook.Controllers.V1
{
    public class PostsController : Controller
    {
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();

            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid()});
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] PostRequest postRequest)
        {
            Guid postId;
            if (!Guid.TryParse(postRequest.Id, out postId))
            {
                postId = new Guid();
            }

            Post post = new Post { Id = postId, Title = postRequest.Title };

            _posts.Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + $"/{ApiRoutes.Posts.Get}";

            var response = new PostResponse { Id = post.Id, Title = post.Title };
            return Created(locationUri, response);
        }
    }
}
