using System;
using System.ComponentModel.DataAnnotations;

namespace TweeterBook.Domain
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
