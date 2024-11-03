using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Models;

namespace DAPM.ResourceRegistryMS.Api.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly ILogger<IUserService> _logger;

        public UserService(ILogger<IUserService> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByMail(String mail)
        {
            return await _userRepository.GetUserByMail(mail);
        }

        public async Task<List<User>?> GetAllUsers(Guid managerId)
        {
            User manager = await _userRepository.GetUserById(managerId);
            if (manager.UserRole != (int)UserRole.Admin)
            {
                return null;
            }

            List<User> users = await _userRepository.GetAllUsers();

            return users;
        }

        public async Task<User?> UpdateAcceptStatus(Guid managerId, Guid userId, int newStatus)
        {
            User manager = await _userRepository.GetUserById(managerId);
            if (manager.UserRole != (int)UserRole.Admin)
            {
                return null;
            }

            User? user = await _userRepository.UpdateAcceptStatus(userId, newStatus);

            return user;
        }

        public async Task<User> PostUser(UserDTO user)
        {
            var u = new User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mail = user.Mail,
                HashPassword = user.HashPassword,
                Organization = user.Organization,
                UserRole = (int)UserRole.User,
                accepted = 0,
            };
            return await _userRepository.AddUser(u);
        }

    }
}
