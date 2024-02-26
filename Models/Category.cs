using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MyBlog
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }
        [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Title { get; set; }
        public string Thumb { get; set; }
        public bool Published { get; set; }
        public int? Ordering { get; set; }
        public int? Parents { get; set; }
        public int? Levels { get; set; }
        public string Icon { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
