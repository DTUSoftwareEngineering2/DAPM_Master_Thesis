using RabbitMQLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQLibrary.Models;

namespace RabbitMQLibrary.Messages.ClientApi
{
    // Author: Maxime Rochat - s241741
    public class GetAvailablePipelinesProcessResult : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid TicketId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public List<PipelineDTO>? pipelines { get; set; }
    }
}
