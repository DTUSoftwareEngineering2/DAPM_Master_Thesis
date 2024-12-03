using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

namespace DAPM.Orchestrator.Consumers.StartProcessConsumers
{
    public class GetAvailablePipelinesRequestConsumer : IQueueConsumer<GetAvailablePipelinesRequest>
    {
        IOrchestratorEngine _engine;
        public GetAvailablePipelinesRequestConsumer(IOrchestratorEngine engine)
        {
            _engine = engine;
        }

        public Task ConsumeAsync(GetAvailablePipelinesRequest message)
        {
            _engine.StartGetAvailablePipelinesProcess(message.TicketId, message.OrganizationId, message.RepositoryId, message.userId);
            return Task.CompletedTask;
        }
    }
}
