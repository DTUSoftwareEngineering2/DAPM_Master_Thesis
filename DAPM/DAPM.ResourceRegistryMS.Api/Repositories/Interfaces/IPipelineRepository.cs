using DAPM.ResourceRegistryMS.Api.Models;

namespace DAPM.ResourceRegistryMS.Api.Repositories.Interfaces
{
    public interface IPipelineRepository
    {
        public Task<Pipeline> AddPipeline(Pipeline pipeline);
        public Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId);
        public Task<IEnumerable<Pipeline>> GetPipelinesFromRepository(Guid organizationId, Guid repositoryId);
        public Task<IEnumerable<Pipeline>> GetSharedPipelines(Guid organizationId);
        public Task<IEnumerable<Pipeline>> GetPipelineStatus(Guid pipelineId);
    }
}
