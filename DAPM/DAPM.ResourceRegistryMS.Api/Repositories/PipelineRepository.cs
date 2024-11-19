using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            _context.SaveChanges();
            return pipeline;
        }

        public async Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            return (Pipeline)_context.Resources.Where(r => r.PeerId == organizationId && r.RepositoryId == repositoryId && r.Id == pipelineId);
        }

        public async Task<IEnumerable<Pipeline>> GetPipelinesFromRepository(Guid organizationId, Guid repositoryId)
        {
            return await _context.Pipelines
                .Where(r => r.PeerId == organizationId && r.RepositoryId == repositoryId)
                .ToListAsync();  // Return a list instead of casting
        }

        public async Task<IEnumerable<Pipeline>> GetSharedPipelines(Guid organizationId)
        {
            return await _context.Pipelines
                .Where(r => r.PeerId == organizationId)
                .ToListAsync();  // Return a list
        }

        public async Task<IEnumerable<Pipeline>> GetPipelineStatus(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            return (IEnumerable<Pipeline>)await _context.Pipelines
                .Where(r => r.PeerId == organizationId && r.RepositoryId == repositoryId && r.Id == pipelineId)
                .FirstOrDefaultAsync();
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
