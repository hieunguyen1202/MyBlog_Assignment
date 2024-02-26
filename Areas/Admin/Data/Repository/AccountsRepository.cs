using MyBlog.Models;
using System.Linq;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly MyBlogContext _context;

        public AccountsRepository(MyBlogContext context)
        {
            _context = context;
        }
        public Account GetAccountByEmail(string email)
        {
           return _context.Accounts.FirstOrDefault(a => a.Email == email);
        }
    }
}
