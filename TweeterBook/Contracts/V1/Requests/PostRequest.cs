using System;

namespace TweeterBook.Contracts.V1.Requests
{
    public class PostRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
