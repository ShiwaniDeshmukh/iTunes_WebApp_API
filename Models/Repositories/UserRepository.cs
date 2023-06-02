using System.Collections.Generic;
using System.Linq;

namespace iTunes_WebApp_API.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> Users = new List<User>
        {
            new User { Username = "shiwani", Password = "shiwani" },
            new User { Username = "apple", Password = "apple" },
            new User { Username = "itunes", Password = "itunes" },
        };

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public bool UserExists(string username)
        {
            return Users.Any(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }
    }
}
