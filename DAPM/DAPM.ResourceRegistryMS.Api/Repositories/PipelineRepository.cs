using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;

namespace DAPM.ResourceRegistryMS.Api.Repositories
{
    public class PipelineRepository : IPipelineRepository
    {
        private readonly ILogger<IPeerRepository> _logger;
        private readonly ResourceRegistryDbContext _context;

        public PipelineRepository(ILogger<IPeerRepository> logger, ResourceRegistryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Pipeline> AddPipeline(Pipeline pipeline)
        {
            await _context.Pipelines.AddAsync(pipeline);
           // _context.SaveChanges();
            return pipeline;
        }

        public async Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            return (Pipeline)_context.Resources.Where(r => r.PeerId == organizationId && r.RepositoryId == repositoryId && r.Id == pipelineId);
        }

        public async Task<IEnumerable<Pipeline>> GetPipelinesFromRepository(Guid organizationId, Guid repositoryId)
        {
            return (Pipeline)_context.Pipelines.Where(r => r.PeerId == organizationId && r.RepositoryId == repositoryId);
        }

        public async Task<IEnumerable<Pipeline>> GetSharedPipelines(Guid organizationId)
        {
            return (Pipeline)_context.Pipelines.Where(r => r.OrganizationId == organizationId);
        }
        public async Task<IEnumerable<Pipeline>> GetPipelineStatus(Guid pipelineId)
        {
            return (Pipeline)_context.Pipelines.Where(r => r.PipelineId == pipelineId);
        }
        
        /*
        public Task<Pipeline> AddPipeline(Pipeline pipeline);
        public Task<IEnumerable<Pipeline>> GetPipelinesFromRepository(Guid organizationId, Guid repositoryId);
        public Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId);
        Task<IEnumerable<Pipeline>> GetSharedPipelines(Guid organizationId);
        Task<IEnumerable<Pipeline>> GetPipelineStatus(Guid pipelineId);
        Task<Pipeline> GetPipelineById(Guid pipelineId);
        */

    }
}
