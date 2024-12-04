using RabbitMQLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RabbitMQLibrary.Messages.Repository
{
    public class GetPipelineExecutionDateResultMessage : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid TicketId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public Guid PipelineId { get; set; }  // The ID of the pipeline
        public List<DateTime>? ExecutionDate { get; set; }  // The execution date (nullable if it hasn't been set yet)
        public Guid ProcessId { get; set; }  // Process ID to correlate with the original request


    }
}