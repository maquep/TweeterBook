using Microsoft.AspNetCore.Identity;
using System;

namespace TweeterBook.Contracts.V1.Responses
{
    public class TagResponse
    {
        public string Name { get; set; }
        public string CreatorId { get; set; }

        public IdentityUser CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
