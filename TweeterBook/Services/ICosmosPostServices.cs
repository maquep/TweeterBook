using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public interface ICosmosPostServices
    {
        Task<List<CosmosPost>> GetPostsAsync();

        Task<CosmosPost> GetPostByIdAsync(Guid Id);

        Task<PostResponse> CreatePostAsync(PostRequest postRequest);

        Task<bool> UpdatePostAsync(CosmosPost post);

        Task<bool> DeletePostByIdAsync(Guid Id);
    }
}
