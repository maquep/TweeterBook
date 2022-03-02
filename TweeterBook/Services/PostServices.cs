using System;
using System.Collections.Generic;
using System.Linq;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public class PostServices : IPostServices
    {
        private List<Post> _posts;

        public PostServices()
        {
            _posts = new List<Post>();

            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid() });
            }
        }

        public PostResponse CreatePost(PostRequest postRequest)
        {
            Guid postId;
            if (!Guid.TryParse(postRequest.Id, out postId))
            {
                postId = Guid.NewGuid();
            }

            var post = new Post { Id = postId, Title = postRequest.Title };
            _posts.Add(post);

            var response = new PostResponse { Id = post.Id, Title = post.Title };

            return response;
        }

        public Post GetPostById(Guid Id)
        {
            return _posts.SingleOrDefault(x => x.Id == Id);
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }
    }
}
