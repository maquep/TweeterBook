using Swashbuckle.AspNetCore.Filters;
using TweeterBook.Contracts.V1.Responses;

namespace TweeterBook.Examples.Responses
{
    public class TagResponseExample : IExamplesProvider<TagResponse>
    {
        public TagResponse GetExamples()
        {
            return new TagResponse
            {
                Name = "New Tag"
            };
        }
    }
}
