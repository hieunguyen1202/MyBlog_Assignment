using System.Collections.Generic;

namespace MyBlog.Models
{
    public class PostDetailViewModel
    {
        public Post Post { get; set; }

        public Category Category { get; set; }
        public IEnumerable<Post> CurrentPost { get; set; }
        public IEnumerable<Comment> CommentList { get; set; }
        public Comment NewComment { get; set; }  // Add this propert
    }
}
