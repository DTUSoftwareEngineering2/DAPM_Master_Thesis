using RabbitMQLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQLibrary.Messages.Repository
{
    // Author: Maxime Rochat - s241741
    public class GetPipelineVisibilityFromRepoMessage : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid ProcessId { get; set; }
        public TimeSpan TimeToLive { get; set; }

        public Guid OrganizationId { get; set; }
        public Guid RepositoryId { get; set; }
        public Guid PipelineId { get; set; }
    }
}
