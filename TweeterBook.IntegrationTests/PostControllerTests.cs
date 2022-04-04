using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TweeterBook.Contracts.V1;
using TweeterBook.Contracts.V1.Requests;
using TweeterBook.Domain;
using Xunit;

namespace TweeterBook.IntegrationTests
{
    public  class PostControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutPosts_ReturnEmptyResponse()
        {
            //Arrange
            await AuthenticatedAsync();

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnPost_WhenPostExists()
        {
            //Arrange
            await AuthenticatedAsync();
            var createdPost = await CreatePostAsync(new CreatePostRequest {Title = "Max Post"});

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("postId", createdPost.Id.ToString()));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnPost = await response.Content.ReadAsAsync<Post>();
            returnPost.Id.Should().Be(createdPost.Id.ToString());
            returnPost.Title.Should().Be("Max Post");
        }
    }
}
