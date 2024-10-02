using System.Collections.Generic;
using DAPM.ClientApi.Models;

namespace DAPM.ClientApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
    }
}
