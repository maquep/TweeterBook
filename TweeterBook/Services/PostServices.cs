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
                _posts.Add(new Post { Id = Guid.NewGuid(), Title = $"Post {i}" });
            }
        }

        public PostResponse CreatePost(PostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id, Title = postRequest.Title };
            _posts.Add(post);

            var response = new PostResponse { Id = post.Id, Title = post.Title };

            return response;
        }

        public bool DeletePostById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(Guid Id)
        {
            return _posts.SingleOrDefault(x => x.Id == Id);
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public bool UpdatePost(Post postToUpdate)
        {
            if(GetPostById(postToUpdate.Id) == null)
                return false;
      
            var postIndex = _posts.FindIndex(x => x.Id == postToUpdate.Id);
            _posts[postIndex] = postToUpdate;

            return true;
        }
    }
}
