using iTunes_WebApp_API.Models;

namespace iTunes_WebApp_API.Models.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsernameAndPassword(string username, string password);
        bool UserExists(string username);
        void AddUser(User user);
    }
}
