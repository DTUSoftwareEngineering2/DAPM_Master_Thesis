using DAPM.RepositoryMS.Api.Models.PostgreSQL;

namespace DAPM.RepositoryMS.Api.Repositories.Interfaces
{
    public interface IPipelineRepository
    {
        Task<Pipeline> AddPipeline(Pipeline pipeline);
        Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId);
        Task<Pipeline> ModifyPipelineById(Guid repositoryId, Guid pipelineId, Pipeline newPipeline);
        Task<Pipeline> DeletePipelineById(Guid repositoryId, Guid pipelineId, Guid userId);
        Task<List<Pipeline>?> GetAvailablePipelines(Guid repositoryId);
    }
}
