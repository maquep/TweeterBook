using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync(PaginationFilter paginationFilter = null);

        Task<Post> GetPostByIdAsync(Guid Id);

        Task<PostResponse> CreatePostAsync(Post postRequest);

        Task<bool> UpdatePostAsync(Post post);

        Task<List<Tag>> GetAllTagsAsync();

        Task<bool> DeletePostByIdAsync(Guid Id);

        Task<bool> UserOwnsPostAsync(Guid postId, string userId);
        Task<bool> DeleteTagAsync(string tagName);
        Task<TagResponse> CreateTagAsync(Tag newTag);
    }
}
