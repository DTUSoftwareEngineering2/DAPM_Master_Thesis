using RabbitMQLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Authors: s242147 and s241747 : Message for setting the execution date of a pipeline
namespace RabbitMQLibrary.Messages.ClientApi
{
    public class SetPipelineExecutionDateRequest : IQueueMessage
    {
        public Guid TicketId { get; set; }
        public Guid PipelineId { get; set; }
        public String ExecutionDate { get; set; }
        public Guid RepositoryId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public Guid MessageId { get; set; }

        public Guid OrganizationId { get; set; }
    }
}