using DAPM.RepositoryMS.Api.Models.PostgreSQL;
using DAPM.RepositoryMS.Api.Repositories.Interfaces;
using DAPM.RepositoryMS.Api.Services.Interfaces;

namespace DAPM.RepositoryMS.Api.Services
{
    public class PipelineService : IPipelineService
    {
        private IPipelineRepository _pipelineRepository;
        private ILogger<PipelineService> _logger;

        public PipelineService(IPipelineRepository pipelineRepository, ILogger<PipelineService> logger)
        {
            _pipelineRepository = pipelineRepository;
            _logger = logger;
        }
        
        public Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId)
        {
            return _pipelineRepository.GetPipelineById(repositoryId, pipelineId);
        }


        public async Task SetPipelineExecutionDate(Guid pipelineId, String executionDate, Guid repositoryId)
        {
            await _pipelineRepository.SetPipelineExecutionDate(pipelineId, executionDate, repositoryId);
        }

        public async Task<List<DateTime>> GetPipelineExecutionDate(Guid pipelineId, Guid repositoryId)
        {
            var pipeline = await _pipelineRepository.GetPipelineById(repositoryId, pipelineId);
            if (pipeline == null)
            {
                _logger.LogError("Pipeline with id {PipelineId} not found", pipelineId);
                return new List<DateTime>();
            }
            return pipeline.ExecutionDate;  
        }
    }
}
