using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public int? PostId { get; set; }
        public bool? Published { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Post Post { get; set; }
    }
}
