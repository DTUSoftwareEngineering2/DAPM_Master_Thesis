﻿using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo
{
    // Author: Maxime Rochat - s241741
    public class PostPipelineToRepoResultMessage : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid ProcessId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public PipelineDTO Pipeline { get; set; }
    }
}
