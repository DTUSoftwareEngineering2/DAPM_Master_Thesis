using DAPM.ClientApi.Models;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DAPM.ClientApi.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("organizations")]
    public class OrganizationController : ControllerBase
    {

        private readonly ILogger<OrganizationController> _logger;
        private readonly IOrganizationService _organizationService;

        public OrganizationController(ILogger<OrganizationController> logger, IOrganizationService organizationService)
        {
            _logger = logger;
            _organizationService = organizationService;
        }

        [HttpGet]
        [SwaggerOperation(Description = "Gets all peers (organizations) you are connected to. There has to be a collaboration agreement " +
            "and a handshake before you can see other organizations using this endpoint.")]
        public async Task<ActionResult<Guid>> Get()
        {
            Guid id = _organizationService.GetOrganizations();
            return Ok(new ApiResponse { RequestName = "GetAllOrganizations", TicketId = id});
        }        
        
        // ApiResponse class remains the same
        public class ApiResponse
        {
            public string RequestName { get; set; }
            public Guid TicketId { get; set; }
            public string TicketTitle { get; set; }
            public string TicketStatus { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        [HttpGet("tickets/{ticketId}")]
        [SwaggerOperation(Description = "Retrieves details of a ticket by its ID.")]
        public async Task<ActionResult<ApiResponse>> GetTicketById(Guid ticketId)
        {
            // Simulate fetching the ticket by its ID from a data store
            var demoTicket = new ApiResponse
            {
                RequestName = "GetTicketById",
                TicketId = ticketId, // Returning the input ID as part of the response
                TicketTitle = "Demo Ticket",
                TicketStatus = "Open",
                CreatedAt = DateTime.UtcNow
            };

            return Ok(demoTicket);
        }

        // Corrected 'testEndpoint'
        [HttpGet("endpoint/{ticketId}")]
        [SwaggerOperation(Description = "Endpoint getter of the backend.")]
        public async Task<ActionResult<ApiResponse>> TestEndpoint(Guid ticketId)
        {
            // Simulating a response similar to the one returned in GetTicketById
            var response = new ApiResponse
            {
                RequestName = "TestEndpoint",
                TicketId = Guid.NewGuid(), // Generate a new ticket ID
                TicketTitle = "Test Ticket", // You can set an appropriate value here
                TicketStatus = "In Progress", // Set a status for the ticket
                CreatedAt = DateTime.UtcNow // Set the current time
            };

            return Ok(response);
        }


                //artur endpoints
        //proper name, proper args, return demi point(some values)
        // [HttpGet("Endpoint")]
        // [SwaggerOperation(Description = "Endpoint getter of the backend.")]
        // public async Task<ActionResult<Guid>> testEndpoint(Guid ticketId)
        // {
        //     return Ok(new ApiResponse { RequestName = "name of endpoint", TicketId = Guid.NewGuid(), TicketTitle = GetTicketById(Guid.NewGuid()), TicketStatus =   });
        // }

        //test pipeline status tokenization
        // [HttpGet("")]
        // [SwaggerOperation(Description = "pipeline status")]
        // public async Task<ActionResult<Guid>> testPipelineStatus()
        // {
        //     return Ok(new ApiResponse { RequestName = "pipeline status", TicketId = Guid.NewGuid()});
        // }
        
        [HttpGet("{organizationId}")]
        [SwaggerOperation(Description = "Gets an organization by id. You need to have a collaboration agreement to retrieve this information.")]
        public async Task<ActionResult<Guid>> GetById(Guid organizationId)
        {
            Guid id = _organizationService.GetOrganizationById(organizationId);
            return Ok(new ApiResponse { RequestName = "GetOrganizationById", TicketId = id });
        }

        [HttpGet("{organizationId}/repositories")]
        [SwaggerOperation(Description = "Gets all the repositories of an organization by id. You need to have a collaboration agreement to retrieve this information.")]
        public async Task<ActionResult<Guid>> GetRepositoriesOfOrganization(Guid organizationId)
        {
            Guid id = _organizationService.GetRepositoriesOfOrganization(organizationId);
            return Ok(new ApiResponse {RequestName = "GetRepositoriesOfOrganization", TicketId = id });
        }

        [HttpPost("{organizationId}/repositories")]
        [SwaggerOperation(Description = "Creates a new repository for an organization by id. Right now you can create repositories for any organizations, but ideally you would " +
            "only be able to create repositories for your own organization.")]
        public async Task<ActionResult<Guid>> PostRepositoryToOrganization(Guid organizationId, [FromBody] RepositoryApiDto repositoryDto)
        {
            Guid id = _organizationService.PostRepositoryToOrganization(organizationId, repositoryDto.Name);
            return Ok(new ApiResponse { RequestName = "PostRepositoryToOrganization", TicketId = id });
        }

    }
}
