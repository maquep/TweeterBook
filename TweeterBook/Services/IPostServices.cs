using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public interface IPostServices
    {
        Task<List<Post>> GetPostsAsync();

        Task<Post> GetPostByIdAsync(Guid Id);

        Task<PostResponse> CreatePostAsync(Post postRequest);

        Task<bool> UpdatePostAsync(Post post);

        Task<bool> DeletePostByIdAsync(Guid Id);

        Task<bool> UserOwnsPostAsync(Guid postId, string userId);
    }
}
