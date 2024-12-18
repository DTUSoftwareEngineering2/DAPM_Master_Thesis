using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

// Author: Maxime Rochat - s241741
namespace DAPM.Orchestrator.Consumers.StartProcessConsumers
{
    public class PostPipelineDeleteRequestConsumer :
        IQueueConsumer<PostPipelineDeleteRequest>
    {
        IOrchestratorEngine _engine;
        public PostPipelineDeleteRequestConsumer(IOrchestratorEngine engine)
        {
            _engine = engine;
        }
        public Task ConsumeAsync(PostPipelineDeleteRequest message)
        {
            _engine.StartGetPipelineVisibility(message.TicketId, message.OrganizationId, message.RepositoryId, message.PipelineId);
            return Task.CompletedTask;
        }
    }
}
