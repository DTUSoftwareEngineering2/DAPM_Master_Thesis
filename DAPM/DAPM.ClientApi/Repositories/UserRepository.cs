using System.Collections.Generic;
using System.Linq;
using DAPM.ClientApi.Models;
using DAPM.ClientApi.Repositories.Interfaces;

namespace DAPM.ClientApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        // In-memory user list (could be replaced with a database)
        private readonly List<User> _users = new List<User>
        {
            new User { UserId = 1, Username = "johndoe", Email = "john.doe@example.com", Role = "Admin", CreatedAt = DateTime.Now.AddMonths(-3), LastLogin = DateTime.Now.AddHours(-2) },
            new User { UserId = 2, Username = "janedoe", Email = "jane.doe@example.com", Role = "User", CreatedAt = DateTime.Now.AddMonths(-2), LastLogin = DateTime.Now.AddDays(-1) }
        };

        public User GetUserById(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }
    }
}
