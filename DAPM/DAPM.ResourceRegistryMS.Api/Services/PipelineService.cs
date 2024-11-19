using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using DAPM.ResourceRegistryMS.Api.Repositories;
using DAPM.PipelineOrchestratorMS.Api.Models;
using System.IO.Pipelines;

namespace DAPM.ResourceRegistryMS.Api.Services
{
    public class PipelineService : IPipelineService
    {
        private IPipelineRepository _pipelineRepository;

        public PipelineService(IPipelineRepository pipelineRepository) 
        {
            _pipelineRepository = pipelineRepository;
        }   
        
        public async Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid resourceId)
        {
            return await _pipelineRepository.GetPipelineById(organizationId, repositoryId, resourceId);
        }

        public async Task<Pipeline> GetPipelineStatus(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            return (Pipeline)await _pipelineRepository.GetPipelineStatus(organizationId, repositoryId, pipelineId); 
        }
    }
}
