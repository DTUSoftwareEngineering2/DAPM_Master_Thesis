using RabbitMQLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo
{
    // Author: Maxime Rochat - s241741
    public class GetPipelineVisibilityFromRepoResult : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid ProcessId { get; set; }
        public TimeSpan TimeToLive { get; set; }

        public Guid PipelineId { get; set; }
        public int visibility { get; set; }
    }
}
