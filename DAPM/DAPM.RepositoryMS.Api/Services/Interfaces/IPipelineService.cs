using DAPM.RepositoryMS.Api.Models.PostgreSQL;

namespace DAPM.RepositoryMS.Api.Services.Interfaces
{
    public interface IPipelineService
    {
        Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId);
        Task<Pipeline> ModifyPipelineById(Guid repositoryId, Guid pipelineId, Pipeline newPipeline, string Name);
    }
}
