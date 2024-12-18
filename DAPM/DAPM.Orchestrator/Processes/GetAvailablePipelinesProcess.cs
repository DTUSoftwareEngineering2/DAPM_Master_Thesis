using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.ResourceRegistry;

namespace DAPM.Orchestrator.Processes
{
    // Author: Maxime Rochat - s241741
    public class GetAvailablePipelinesProcess : OrchestratorProcess
    {
        private Guid _organizationId;
        private Guid _repositoryId;
        private Guid? _userId;

        private Guid _ticketId;
        public GetAvailablePipelinesProcess(OrchestratorEngine engine, IServiceProvider serviceProvider, Guid ticketId, Guid processId,
            Guid organizationId, Guid repositoryId, Guid? userId)
            : base(engine, serviceProvider, processId)
        {
            _organizationId = organizationId;
            _repositoryId = repositoryId;
            _userId = userId;

            _ticketId = ticketId;
        }

        public override void StartProcess()
        {


            var getAvailablePipelinesFromRepoProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetAvailablePipelinesFromRepoMessage>>();

            var message = new GetAvailablePipelinesFromRepoMessage()
            {
                ProcessId = _processId,
                MessageId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                RepositoryId = _repositoryId,
            };

            getAvailablePipelinesFromRepoProducer.PublishMessage(message);

        }

        public override void OnGetAvailablePipelinesFromRepoResult(GetAvailablesPipelinesFromRepoResultMessage message)
        {
            var getAvailablePipelinesProcessResult = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetAvailablePipelinesProcessResult>>();

            var resultMessage = new GetAvailablePipelinesProcessResult()
            {
                MessageId = message.MessageId,
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                pipelines = message.Pipelines,
            };

            getAvailablePipelinesProcessResult.PublishMessage(resultMessage);
        }
    }
}
