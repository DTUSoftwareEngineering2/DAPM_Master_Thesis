using DAPM.ResourceRegistryMS.Api.Models;
using RabbitMQLibrary.Models;

namespace DAPM.ResourceRegistryMS.Api.Services.Interfaces
{
    // Author: Maxime Rochat - s241741
    public interface IUserService
    {
        Task<User> GetUserById(Guid id);
        Task<List<User>?> GetAllUsers(Guid managerId);
        Task<User?> UpdateAcceptStatus(Guid managerId, Guid userId, int newStatus, int role);
        Task<User?> DeleteUser(Guid managerId, Guid userId);
        Task<User> GetUserByMail(String mail);
        Task<User> PostUser(UserDTO user);

    }
}
