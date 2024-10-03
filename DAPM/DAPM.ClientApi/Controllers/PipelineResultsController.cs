using DAPM.ClientApi.Models;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using Swashbuckle.AspNetCore.Annotations;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PipelineResultsController : ControllerBase
    {
        //private readonly IPipelineResultsService _pipelineResultsService;

        public PipelineResultsController()
        {
            //_pipelineResultsService = pipelineResultsService;
        }

        [HttpGet("GetAllResults")]
        [SwaggerOperation(
            Summary = "Get all pipeline results",
            Description = "Retrieves a complete list of all pipeline execution results available in the system.",
            OperationId = "GetAllPipelineResults",
            Tags = new[] { "PipelineResults" }
        )]
        public async Task<IActionResult> GetAllPipelineResults()
        {

            return Ok("Version 0.0.0");
            /**
            var results = await _pipelineResultsService.GetAllPipelineResultsAsync();
            return Ok(results);
            **/
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a specific pipeline result by ID",
            Description = "Retrieves the details of a pipeline result using its unique identifier.",
            OperationId = "GetPipelineResultById",
            Tags = new[] { "PipelineResults" }
        )]
        public async Task<IActionResult> GetPipelineResultById(string id)
        {

            return Ok("Version 0.0.0");
            /*
            var result = await _pipelineResultsService.GetPipelineResultByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
            */
        }

        [HttpGet("ByExecution/{executionId}")]
        [SwaggerOperation(
            Summary = "Get pipeline result by execution ID",
            Description = "Retrieves the details of a pipeline result using its execution ID.",
            OperationId = "GetPipelineResultByExecutionId",
            Tags = new[] { "PipelineResults" }
        )]
        public async Task<IActionResult> GetPipelineResultByExecutionId(string executionId)
        {

            return Ok("Version 0.0.0");
            /*
            var result = await _pipelineResultsService.GetPipelineResultByExecutionIdAsync(executionId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
            */
        }
    }
}
