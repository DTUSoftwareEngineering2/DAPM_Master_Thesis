using DAPM.RepositoryMS.Api.Models.PostgreSQL;

namespace DAPM.RepositoryMS.Api.Repositories.Interfaces
{
    public interface IPipelineRepository
    {
        Task<Pipeline> AddPipeline(Pipeline pipeline);
        Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId);
        Task<Pipeline> ModifyPipelineById(Guid repositoryId, Guid pipelineId, Pipeline newPipeline);
        Task<List<Pipeline>?> GetAvailablePipelines(Guid repositoryId);
    }
}
