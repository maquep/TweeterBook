using System;
using System.Collections.Generic;
using TweeterBook.Domain;

namespace TweeterBook.Contracts.V1.Requests
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
