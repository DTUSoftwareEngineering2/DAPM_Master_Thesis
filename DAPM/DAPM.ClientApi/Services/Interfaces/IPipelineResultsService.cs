using System.Threading.Tasks;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IPipelineResultsService
    {
        Guid GetAllPipelineResultsAsync();
        Guid GetPipelineResultByIdAsync(string id);
        Guid GetPipelineResultByExecutionIdAsync(string executionId);
    }
}
