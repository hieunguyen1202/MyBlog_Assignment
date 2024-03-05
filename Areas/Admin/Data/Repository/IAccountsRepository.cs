using MyBlog.Models;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public interface IAccountsRepository
    {
        Account GetAccountByEmail(string email);
        Account GetAccountById(int id);

    }
}
