using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Contents is required")]
        [MinLength(3, ErrorMessage = "Contents must be at least 3 characters long")]
        public string Contents { get; set; }
        public string Thumb { get; set; }
        public bool Published { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Author { get; set; }
        [Required(ErrorMessage = "Short Content is required")]
        [MinLength(3, ErrorMessage = "Short Content must be at least 3 characters long")]
        [MaxLength(100, ErrorMessage = "Short Content maximum is 100 characters")]
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
