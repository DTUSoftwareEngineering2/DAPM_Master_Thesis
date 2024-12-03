using DAPM.RepositoryMS.Api.Models.PostgreSQL;

namespace DAPM.RepositoryMS.Api.Services.Interfaces
{
    public interface IPipelineService
    {
        Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId);
        Task SetPipelineExecutionDate(Guid pipelineId, String executionDate, Guid repositoryId);
        Task<List<DateTime>> GetPipelineExecutionDate(Guid pipelineId, Guid repositoryId);
        Task<Pipeline> ModifyPipelineById(Guid repositoryId, Guid pipelineId, Pipeline newPipeline, string Name);
    }
}
