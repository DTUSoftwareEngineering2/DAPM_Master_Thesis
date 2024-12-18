using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.ResourceRegistry;

namespace DAPM.Orchestrator.Processes
{
    // Author: Maxime Rochat - s241741
    public class GetPipelineVisibilityProcess : OrchestratorProcess
    {
        private Guid _organizationId;
        private Guid _repositoryId;
        private Guid _pipelineId;

        private Guid _ticketId;
        public GetPipelineVisibilityProcess(OrchestratorEngine engine, IServiceProvider serviceProvider, Guid ticketId, Guid processId,
            Guid organizationId, Guid repositoryId, Guid pipelineId)
            : base(engine, serviceProvider, processId)
        {
            _organizationId = organizationId;
            _repositoryId = repositoryId;
            _pipelineId = pipelineId;

            _ticketId = ticketId;
        }

        public override void StartProcess()
        {

            var getPipelineVisiblityFromRepoMessageQueue = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetPipelineVisibilityFromRepoMessage>>();

            var message = new GetPipelineVisibilityFromRepoMessage()
            {
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                RepositoryId = _repositoryId,
                OrganizationId = _organizationId,
                PipelineId = _pipelineId,
            };

            getPipelineVisiblityFromRepoMessageQueue.PublishMessage(message);

        }

        public override void OnGetPipelineVisibilityFromRepoResult(GetPipelineVisibilityFromRepoResult message)
        {

            var getPipelineVisibilityResultProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetPipelineVisibilityResult>>();

            var resultMessage = new GetPipelineVisibilityResult()
            {
                MessageId = Guid.NewGuid(),
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),

                pipelineId = message.PipelineId,
                visbility = message.visibility,
            };

            getPipelineVisibilityResultProducer.PublishMessage(resultMessage);

        }

    }
}
