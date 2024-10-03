using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DAPM.PipelineOrchestratorMS.Api.Models;
using DAPM.PipelineOrchestratorMS.Api.Services.Interfaces;

namespace DAPM.PipelineOrchestratorMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineResultsController : ControllerBase
    {
        private readonly IPipelineResultsService _pipelineResultsService;

        public PipelineResultsController(IPipelineResultsService pipelineResultsService)
        {
            _pipelineResultsService = pipelineResultsService;
        }

        // Endpoint to get all results for a specific pipeline ID 
        // Im not sure if a pipeline can have multiple results but just in case
        [HttpGet("{pipelineId}")]
        public ActionResult<IEnumerable<PipelineExecutionStatus>> GetPipelineResults(int pipelineId)
        {
            var results = _pipelineResultsService.GetResultsByPipelineId(pipelineId);
            if (results == null || results.Count == 0)
            {
                return NotFound($"No results found for Pipeline ID {pipelineId}");
            }
            return Ok(results);
        }

        // Endpoint to get a specific result by pipeline ID and result ID
        [HttpGet("{pipelineId}/result/{executionId}")]
        public ActionResult<PipelineExecutionStatus> GetPipelineResultById(int pipelineId, int executionId)
        {
            var result = _pipelineResultsService.GetResultById(pipelineId, executionId);
            if (result == null)
            {
                return NotFound($"Result with ID {executionId} not found for Pipeline ID {pipelineId}");
            }
            return Ok(result);
        }
    }
}
