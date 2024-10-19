using System.Threading.Tasks;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IPipelineResultsService
    {
        Guid GetAllPipelineResultsAsync(Guid organizationId, Guid repositoryId, Guid resourceId);
        Guid GetPipelineResultByIdAsync(Guid organizationId, Guid repositoryId, Guid resourceId);
    }
}
