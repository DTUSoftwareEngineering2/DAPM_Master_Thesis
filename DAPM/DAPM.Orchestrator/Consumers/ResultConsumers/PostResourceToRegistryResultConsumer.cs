﻿using DAPM.Orchestrator.Processes;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults;

namespace DAPM.Orchestrator.Consumers.ResultConsumers
{
    public class PostResourceToRegistryResultConsumer : IQueueConsumer<PostResourceToRegistryResultMessage>
    {
        private IOrchestratorEngine _orchestratorEngine;

        public PostResourceToRegistryResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(PostResourceToRegistryResultMessage message)
        {
            PostResourceProcess process = (PostResourceProcess)_orchestratorEngine.GetProcess(message.TicketId);
            process.OnPostResourceToRegistryResult(message);

            return Task.CompletedTask;
        }
    }
}
