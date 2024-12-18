using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.ResourceRegistry;

namespace DAPM.Orchestrator.Processes
{
    // Author: Maxime Rochat - s241741
    public class PostPipelineDeleteProcess : OrchestratorProcess
    {
        private Guid _organizationId;
        private Guid _repositoryId;
        private Guid _pipelineId;
        private Guid _userId;

        private Guid _ticketId;
        public PostPipelineDeleteProcess(OrchestratorEngine engine, IServiceProvider serviceProvider, Guid ticketId, Guid processId,
            Guid organizationId, Guid repositoryId, Guid pipelineId, Guid userId)
            : base(engine, serviceProvider, processId)
        {
            _organizationId = organizationId;
            _repositoryId = repositoryId;
            _pipelineId = pipelineId;
            _userId = userId;

            _ticketId = ticketId;
        }

        public override void StartProcess()
        {

            var postPipelineDeleteFromRepoMessageQueue = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<PostPipelineDeleteToRepoMessage>>();

            var message = new PostPipelineDeleteToRepoMessage()
            {
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                RepositoryId = _repositoryId,
                OrganizationId = _organizationId,
                PipelineId = _pipelineId,
                UserId = _userId,
            };

            postPipelineDeleteFromRepoMessageQueue.PublishMessage(message);

        }

        public override void OnGetAvailablePipelinesFromRepoResult(GetAvailablesPipelinesFromRepoResultMessage message)
        {
            var getAvailablePipeline = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetAvailablePipelinesProcessResult>>();

            var resultMessage = new GetAvailablePipelinesProcessResult()
            {
                MessageId = message.MessageId,
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                pipelines = message.Pipelines,

            };

            getAvailablePipeline.PublishMessage(resultMessage);

        }

    }
}
