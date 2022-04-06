using Swashbuckle.AspNetCore.Filters;
using TweeterBook.Contracts.V1.Requests;

namespace TweeterBook.Examples.Requests
{
    public class CreateTagRequestExample : IExamplesProvider<CreateTagRequest>
    {
        public CreateTagRequest GetExamples()
        {
            return new CreateTagRequest
            { 
                Name = "new tag"
            };
        }
    }
}
