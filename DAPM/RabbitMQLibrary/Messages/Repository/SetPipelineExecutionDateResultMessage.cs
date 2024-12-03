using RabbitMQLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQLibrary.Messages.Repository
{
    public class SetPipelineExecutionDateResultMessage : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public Guid PipelineId { get; set; }  // The ID of the pipeline
        public Guid TicketId { get; set; }  // Ticket ID to correlate with the original request
        public bool Success { get; set; }  // Indicates if the operation was successful
        public string Message { get; set; }  // Optional message for additional information
    }
}   