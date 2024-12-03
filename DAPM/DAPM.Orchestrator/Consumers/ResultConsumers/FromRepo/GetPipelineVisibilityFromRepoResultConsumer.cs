using DAPM.Orchestrator.Processes;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

namespace DAPM.Orchestrator.Consumers.ResultConsumers.FromRepo
{
    public class GetPipelineVisibilityFromRepoResultConsumer : IQueueConsumer<GetPipelineVisibilityFromRepoResult>
    {
        private IOrchestratorEngine _orchestratorEngine;

        public GetPipelineVisibilityFromRepoResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(GetPipelineVisibilityFromRepoResult message)
        {
            OrchestratorProcess process = _orchestratorEngine.GetProcess(message.ProcessId);
            process.OnGetPipelineVisibilityFromRepoResult(message);

            return Task.CompletedTask;
        }
    }
}
