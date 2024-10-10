using System.Threading.Tasks;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IPipelineResultsService
    {
        Task<string> GetAllPipelineResultsAsync();
        Task<string> GetPipelineResultByIdAsync(string id);
        Task<string> GetPipelineResultByExecutionIdAsync(string executionId);
        Task<string> GetPipelineResultTextByIdAsync(string id);
    }
}
