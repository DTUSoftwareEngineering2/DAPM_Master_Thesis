using DAPM.Orchestrator.Processes;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

// Author: Maxime Rochat - s241741
namespace DAPM.Orchestrator.Consumers.ResultConsumers.FromRepo
{
    public class GetAvailablePipelinesFromRepoResultConsumer : IQueueConsumer<GetAvailablesPipelinesFromRepoResultMessage>
    {
        private IOrchestratorEngine _orchestratorEngine;

        public GetAvailablePipelinesFromRepoResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(GetAvailablesPipelinesFromRepoResultMessage message)
        {
            OrchestratorProcess process = _orchestratorEngine.GetProcess(message.ProcessId);
            process.OnGetAvailablePipelinesFromRepoResult(message);

            return Task.CompletedTask;
        }
    }
}
