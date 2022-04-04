using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TweeterBook.Domain
{
    public class PostTag
    {
        [ForeignKey(nameof(TagName))]
        public virtual Tag Tag { get; set; }

        [Key]
        public string TagName { get; set; }

        public virtual Post Post { get; set; }

        public Guid PostId { get; set; }
    }
}
