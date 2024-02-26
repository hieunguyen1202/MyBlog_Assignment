using System.Collections.Generic;

namespace MyBlog.Models
{
    public class IndexViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Post> Posts { get; set; }
    }
}
