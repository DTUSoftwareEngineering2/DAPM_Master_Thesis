﻿using DAPM.ResourceRegistryMS.Api.Models;
using RabbitMQLibrary.Models;

namespace DAPM.ResourceRegistryMS.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(Guid id);

        Task<User> GetUserByMail(String mail);
        Task<User> PostUser(UserDTO user);

    }
}
