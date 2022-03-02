using System;
using System.Collections.Generic;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public interface IPostServices
    {
        List<Post> GetPosts();

        Post GetPostById(Guid Id);

        PostResponse CreatePost(PostRequest postRequest);

        bool UpdatePost(Post post);

        bool DeletePostById(Guid Id);
    }
}
