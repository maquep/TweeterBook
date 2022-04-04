using System;
using System.Collections.Generic;

namespace TweeterBook.Contracts.V1.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public List<TagResponse> Tags { get; set; }
    }
}
