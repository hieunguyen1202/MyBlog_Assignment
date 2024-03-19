using System.Collections.Generic;

namespace MyBlog.Models
{
    public class EditPostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
