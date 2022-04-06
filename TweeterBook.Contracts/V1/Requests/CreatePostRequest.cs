using System;
using System.Collections.Generic;

namespace TweeterBook.Contracts.V1.Requests
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public List<CreateTagRequest> Tags { get; set; }
    }
}
