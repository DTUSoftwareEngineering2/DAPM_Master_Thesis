using DAPM.ResourceRegistryMS.Api.Models;

namespace DAPM.ResourceRegistryMS.Api.Repositories.Interfaces
{
    // Author: Maxime Rochat - s241741
    public interface IUserRepository
    {
        public Task<User> GetUserByMail(String mail);
        public Task<User> GetUserById(Guid id);
        public Task<User> AddUser(User user);
        public Task<List<User>> GetAllUsers();
        public Task<User?> UpdateAcceptStatus(Guid id, int newStatus, int role);
        public Task<User?> DeleteUser(Guid id);
    }
}
