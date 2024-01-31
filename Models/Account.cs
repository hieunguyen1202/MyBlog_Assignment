﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MyBlog
{
    public partial class Account
    {
        public Account()
        {
            Posts = new HashSet<Post>();
        }

        public int AccountId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}