using DAPM.RepositoryMS.Api.Data;
using DAPM.RepositoryMS.Api.Models.PostgreSQL;
using DAPM.RepositoryMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAPM.RepositoryMS.Api.Repositories
{
    public class PipelineRepository : IPipelineRepository
    {
        private ILogger<PipelineRepository> _logger;
        private readonly RepositoryDbContext _repositoryDbContext;

        public PipelineRepository(ILogger<PipelineRepository> logger,  RepositoryDbContext repositoryDbContext)
        {
            _logger = logger;
            _repositoryDbContext = repositoryDbContext;
        }

        public async Task<Pipeline> AddPipeline(Pipeline pipeline)
        {
            await _repositoryDbContext.Pipelines.AddAsync(pipeline);
            _repositoryDbContext.SaveChanges();
            return pipeline;
        }

        public async Task SetPipelineExecutionDate(Guid pipelineId, String executionDate, Guid repositoryId)
        {
            var pipeline = await _repositoryDbContext.Pipelines.FirstOrDefaultAsync(p => p.Id == pipelineId && p.RepositoryId == repositoryId);
            if (pipeline == null)
            {
                throw new Exception("Pipeline not found");
            }

            DateTime parsedDateTime = DateTime.Parse(executionDate);
            if (parsedDateTime.Kind == DateTimeKind.Unspecified)
            {
                parsedDateTime = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc);
            }
            else
            {
                parsedDateTime = parsedDateTime.ToUniversalTime();
            }

            if (pipeline.ExecutionDate == null)
            {
                pipeline.ExecutionDate = new List<DateTime> { parsedDateTime };
            }
            else
            {
                pipeline.ExecutionDate.Add(parsedDateTime);
            }
            _repositoryDbContext.SaveChanges();
        }

        public async Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId)
        {
            return await _repositoryDbContext.Pipelines.FirstOrDefaultAsync(p => p.Id == pipelineId && p.RepositoryId == repositoryId);
        }

       
    }
}
