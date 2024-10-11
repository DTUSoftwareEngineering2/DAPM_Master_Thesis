using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAPM.ResourceRegistryMS.Api.Models
{
    public class Pipeline
    {
        // Attributes (Columns)
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid PeerId { get; set; }
        public Guid RepositoryId { get; set; }


        // Navigation Attributes (Foreign Keys)

        [ForeignKey("PeerId")]
        public virtual Peer Peer { get; set; }
        [ForeignKey("PeerId, RepositoryId")]
        public virtual Repository Repository { get; set; }
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

        public class Pipeline StatusChange {
            
        }

}
