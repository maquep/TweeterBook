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
            post.Tags?.ForEach(x => x.TagName = x.TagName.ToLower());
            await AddNewTagsAsync(post);

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

        /// <summary>
        /// Get all Posts for all users if userid id null
        /// Get all posts for specific user if userid is not null
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public async Task<List<Post>> GetPostsAsync(GetAllPostsFilter filter = null, PaginationFilter paginationFilter = null)
        {
            var queryable = _dataContext.Posts.AsQueryable();

            queryable = AddFilterOnQuery(filter, queryable);

            if (paginationFilter == null)
            {
                return await queryable.Include(x => x.Tags).ToListAsync();
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable.Include(x => x.Tags)
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToListAsync<Post>();
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

        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await _dataContext.Tags.SingleOrDefaultAsync(x => x.Name == tagName);
        }

        public async Task<bool> DeleteTagAsync(string tagName)
        {
            var tag = await GetTagByNameAsync(tagName);

            if (tag == null)
                return false;

            _dataContext.Tags.Remove(tag);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<TagResponse> CreateTagAsync(Tag newTag)
        {
            await _dataContext.Tags.AddAsync(newTag);
            var created = await _dataContext.SaveChangesAsync();

            if (created == 0)
                return null;

            var tagResponse = new TagResponse
            {
                Name = newTag.Name
            };

            return tagResponse;
        }

        private async Task AddNewTagsAsync(Post post)
        {
            foreach (var tag in post.Tags)
            {
                var existingTag =
                    await _dataContext.Tags.SingleOrDefaultAsync(x =>
                        x.Name == tag.TagName);
                if (existingTag != null)
                    continue;

                await _dataContext.Tags.AddAsync(new Tag
                { Name = tag.TagName, CreatedOn = DateTime.UtcNow, CreatorId = post.UserId });
            }
        }

        private static IQueryable<Post> AddFilterOnQuery(GetAllPostsFilter filter, IQueryable<Post> queryable)
        {
            if (!string.IsNullOrEmpty(filter?.UserId))
            {
                queryable = queryable.Where(x => x.UserId == filter.UserId);
            }

            return queryable;
        }
    }
}
