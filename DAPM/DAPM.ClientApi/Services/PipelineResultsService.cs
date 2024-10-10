using System.Threading.Tasks;
using DAPM.ClientApi.Services.Interfaces;

namespace DAPM.ClientApi.Services
{
    public class PipelineResultsService : IPipelineResultsService
    {

        // Simulated in-memory storage of pipeline results
        private readonly List<PipelineResultDto> _pipelineResults;

        public PipelineResultsService()
        {
            // Initializing with sample data for demonstration 
            _pipelineResults = new List<PipelineResultDto>
            {
                new PipelineResultDto { Id = "1", ExecutionId = "EXE-001", ResultText = "Pipeline 1 completed successfully." },
                new PipelineResultDto { Id = "2", ExecutionId = "EXE-002", ResultText = "Pipeline 2 failed due to missing resources." },
                new PipelineResultDto { Id = "3", ExecutionId = "EXE-003", ResultText = "Pipeline 3 completed with warnings." }
            };
        }

        public async Task<string> GetAllPipelineResultsAsync()
        {
            // Retrieve all pipeline results and return as a formatted string
            var results = _pipelineResults
                .Select(r => $"ID: {r.Id}, Execution ID: {r.ExecutionId}, Result: {r.ResultText}")
                .ToList();
            
            // Convert to a single string for easier viewing
            return await Task.FromResult(string.Join("\n", results));
        }

        public async Task<string> GetPipelineResultByIdAsync(string id)
        {
            // Find the pipeline result with the specified ID
            var result = _pipelineResults.FirstOrDefault(r => r.Id == id);
            return await Task.FromResult(result?.ResultText ?? $"No pipeline result found with ID: {id}");
        }

        public async Task<string> GetPipelineResultByExecutionIdAsync(string executionId)
        {
            // Find the pipeline result with the specified Execution ID
            var result = _pipelineResults.FirstOrDefault(r => r.ExecutionId == executionId);
            return await Task.FromResult(result?.ResultText ?? $"No pipeline result found with Execution ID: {executionId}");
        }

        public async Task<string> GetPipelineResultTextByIdAsync(string id)
        {
            // Return the plain text result of a specific pipeline by ID
            var result = _pipelineResults.FirstOrDefault(r => r.Id == id);
            return await Task.FromResult(result?.ResultText ?? $"No pipeline result found with ID: {id}");
        }
    }

     // Data Transfer Object for Pipeline Results
    public class PipelineResultDto
    {
        public string Id { get; set; }
        public string ExecutionId { get; set; }
        public string ResultText { get; set; }
    }
}
