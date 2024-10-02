using System.Collections.Generic;
using DAPM.ClientApi.Models;
using DAPM.ClientApi.Repositories.Interfaces;
using DAPM.ClientApi.Services.Interfaces;

namespace DAPM.ClientApi.Services
{
    public class UserService : IUserService
    {
        // Private read-only field for the user repository.
        // This repository handles data access logic for user entities.
        private readonly IUserRepository _userRepository;

        // Constructor to inject the `IUserRepository` dependency into the service.
        public UserService(IUserRepository userRepository)
        {
            // Assign the injected repository to the local field.
            _userRepository = userRepository;
        }

        // Method to get a single user by their ID.
        // This method calls the `GetUserById` method of the `_userRepository`.
        public User GetUserById(int userId)
        {
            // Retrieve a user from the repository based on the provided `userId`.
            return _userRepository.GetUserById(userId);
        }

        // Method to get all users.
        // This method calls the `GetAllUsers` method of the `_userRepository`.
        public IEnumerable<User> GetAllUsers()
        {
            // Retrieve all users from the repository and return them.
            return _userRepository.GetAllUsers();
        }
    }
}

