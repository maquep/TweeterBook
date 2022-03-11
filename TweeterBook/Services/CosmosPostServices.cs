using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Contracts.V1.Responses;
using TweeterBook.Data;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public class CosmosPostServices : ICosmosPostServices
    {
        private readonly CosmosDataContext _dataContext;

        public CosmosPostServices(CosmosDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest postRequest)
        {
            var cosmosPost = new CosmosPost
            {
                Id = Guid.NewGuid().ToString(),
                Title = postRequest.Title
            };

            await _dataContext.Posts.AddAsync(cosmosPost);
            var created  = _dataContext.SaveChanges();

            if (created > 0)
              return new PostResponse { Id = Guid.Parse(cosmosPost.Id), Title = cosmosPost.Title };

            return null;
        }

        public async Task<bool> DeletePostByIdAsync(Guid Id)
        {
            var post = await GetPostByIdAsync(Id);

            if (post == null)
                return false;

            var cosmosPost = new CosmosPost
            {
                Id = post.Id.ToString(),
                Title = post.Title
            };

            _dataContext.Posts.Remove(cosmosPost);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<CosmosPost> GetPostByIdAsync(Guid Id)
        {
            var cosmosPost = await _dataContext.Posts.SingleOrDefaultAsync<CosmosPost>(x => x.Id == Id.ToString());

            return cosmosPost;
        }

        public async Task<List<CosmosPost>> GetPostsAsync()
        {
            var cosmosPosts = await _dataContext.Posts.ToListAsync<CosmosPost>();
            //var posts = new List<CosmosPost>();

            //foreach (var cpost in cosmosPosts)
            //{
            //    posts.Add(new Post { Id = Guid.Parse(cpost.Id), Title = cpost.Title});    
            //}

            return cosmosPosts;
        }

        public async Task<bool> UpdatePostAsync(CosmosPost cosmosPost)
        {
            _dataContext.Posts.Update(cosmosPost);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}
