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
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<PostResponse> CreatePostAsync(Post post)
        {
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

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);

            if (post == null)
            {
                return false;
            }

            if (post.UserId != userId)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dataContext.Tags.AsNoTracking().ToListAsync();
        }

        private async Task AddNewTagsAsync(Post post)
        {
            foreach (var tag in post.Tags)
            {
                var existingTag =
                    await _dataContext.Tags.SingleOrDefaultAsync(x =>
                        x.Name == tag.Name);
                if (existingTag != null)
                    continue;

                await _dataContext.Tags.AddAsync(new Tag
                { Name = tag.Name, CreatedOn = DateTime.UtcNow, CreatorId = post.UserId });
            }
        }
    }
}
