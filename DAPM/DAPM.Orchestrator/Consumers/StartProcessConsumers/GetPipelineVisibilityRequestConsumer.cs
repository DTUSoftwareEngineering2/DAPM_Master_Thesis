using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

// Author: Maxime Rochat - s241741
namespace DAPM.Orchestrator.Consumers.StartProcessConsumers
{
    public class GetPipelineVisibilityRequestConsumer :
        IQueueConsumer<GetPipelineVisibilityRequest>
    {
        IOrchestratorEngine _engine;
        public GetPipelineVisibilityRequestConsumer(IOrchestratorEngine engine)
        {
            _engine = engine;
        }
        public Task ConsumeAsync(GetPipelineVisibilityRequest message)
        {
            _engine.StartGetPipelineVisibility(message.TicketId, message.OrganizationId, message.RepositoryId, message.PipelineId);
            return Task.CompletedTask;
        }
    }
}
