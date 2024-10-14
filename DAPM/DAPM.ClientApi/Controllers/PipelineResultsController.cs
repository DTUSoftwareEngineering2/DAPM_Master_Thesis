using DAPM.ClientApi.Models;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using Swashbuckle.AspNetCore.Annotations;
using RabbitMQLibrary.Models;
using System.Threading.Tasks;

namespace DAPM.ClientApi.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("organizations/")]
    
    public class PipelineResultsController : ControllerBase
    {

        private readonly ILogger<PipelineResultsController> _logger;
        private readonly IPipelineResultsService _pipelineResultsService;

        public PipelineResultsController(ILogger<PipelineResultsController> logger, IPipelineResultsService pipelineResultsService)
        {
            _pipelineResultsService = pipelineResultsService;
            _logger = logger;
        }

        [HttpGet("GetAllResults")]
        [SwaggerOperation(
            Summary = "Get all pipeline results",
            Description = "Retrieves a complete list of all pipeline execution results available in the system.",
            OperationId = "GetAllPipelineResults",
            Tags = new[] { "PipelineResults" }
        )]
        public async Task<ActionResult<Guid>> GetAllPipelineResults()
        {

            //return Ok("Version 0.0.0");
            // var results = await _pipelineResultsService.GetAllPipelineResultsAsync();
            // return Ok(results);

            Guid id = _pipelineResultsService.GetAllPipelineResultsAsync(); ;
            return Ok(new ApiResponse { RequestName = "GetAllPipelineResults", TicketId = id });
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a specific pipeline result by ID",
            Description = "Retrieves the details of a pipeline result using its unique identifier.",
            OperationId = "GetPipelineResultById",
            Tags = new[] { "PipelineResults" }
        )]
        public async Task<ActionResult<Guid>> GetPipelineResultById(string id)
        {

            //return Ok("Version 0.0.0");
            // var result = await _pipelineResultsService.GetPipelineResultByIdAsync(id);
            // if (result == null)
            // {
            //     return NotFound();
            // }
            // return Ok(result);

            Guid idd = _pipelineResultsService.GetPipelineResultByIdAsync(id);
            return Ok(new ApiResponse { RequestName = "GetPipelineResultById", TicketId = idd });
        }

        [HttpGet("ByExecution/{executionId}")]
        [SwaggerOperation(
            Summary = "Get pipeline result by execution ID",
            Description = "Retrieves the details of a pipeline result using its execution ID.",
            OperationId = "GetPipelineResultByExecutionId",
            Tags = new[] { "PipelineResults" }
        )]
        public async Task<ActionResult<Guid>> GetPipelineResultByExecutionId(string executionId)
        {

            //return Ok("Version 0.0.0");
            // var result = await _pipelineResultsService.GetPipelineResultByExecutionIdAsync(executionId);
            // if (result == null)
            // {
            //     return NotFound();
            // }
            // return Ok(result);

            Guid id = _pipelineResultsService.GetPipelineResultByExecutionIdAsync(executionId);
            return Ok(new ApiResponse { RequestName = "GetPipelineResultByExecutionId", TicketId = id });
        }
    }
}
