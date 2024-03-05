using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Thumb { get; set; }
        public bool Published { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Author { get; set; }
        public string Scontent { get; set; }
        public int? CatId { get; set; }
        public bool IsHot { get; set; }
        public bool IsNewFeed { get; set; }
        public int? AccountId { get; set; }
        public string Tags { get; set; }

        public virtual Account Account { get; set; }
        public virtual Category Cat { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
