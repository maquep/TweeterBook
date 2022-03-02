using System;

namespace TweeterBook.Contracts.V1.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
