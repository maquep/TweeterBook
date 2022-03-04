using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Data;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public class PostServices : IPostServices
    {
        private readonly DataContext _dataContext;

        public PostServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<PostResponse> CreatePostAsync(PostRequest postRequest)
        {
            
            var post = new Post { Title = postRequest.Title };
            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();

            var postResponse = new PostResponse { Id = post.Id, Title = post.Title };

            return postResponse;
        }

        public async Task<bool> DeletePostByIdAsync(Guid Id)
        {
            var post = GetPostByIdAsync(Id);

            if (post == null)
                return false;

           _dataContext.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<Post> GetPostByIdAsync(Guid Id)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _dataContext.Posts.ToListAsync<Post>();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
             _dataContext.Posts.Update(postToUpdate);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}
