﻿using DAPM.ResourceRegistryMS.Api.Models;
using RabbitMQLibrary.Models;

namespace DAPM.ResourceRegistryMS.Api.Services.Interfaces
{
    public interface IPeerService
    {
        Task<Peer> GetPeer(Guid id);

        Task<IEnumerable<Peer>> GetAllPeers();

        Task<Peer> PostPeer(OrganizationDTO organizationDTO);
        Task<IEnumerable<Repository>> GetRepositoriesOfOrganization(Guid organizationId);
        Task<Repository> PostRepositoryToOrganization(Guid organizationId, RepositoryDTO repository); 
    }
}
