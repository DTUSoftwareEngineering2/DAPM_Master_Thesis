using System.Collections.Generic;
using DAPM.ClientApi.Models;

namespace DAPM.ClientApi.Services.Interfaces
{
    // Interface definition for the User Service.
    // This interface defines a contract that any implementing class must fulfill.
    public interface IUserService
    {
        // Method signature to retrieve a single user by their ID.
        // Takes an integer parameter `userId` and returns a `User` object.
        User GetUserById(int userId);

        // Method signature to retrieve all users.
        // Returns an `IEnumerable<User>`, representing a collection of `User` objects.
        IEnumerable<User> GetAllUsers();
    }
}
