using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Messages.ClientApi;

namespace DAPM.ClientApi.Services
{
    public class PipelineService : IPipelineService
    {
        private readonly ILogger<PipelineService> _logger;
        private readonly ITicketService _ticketService;
        private readonly IQueueProducer<GetPipelinesRequest> _getPipelinesRequestProducer;
        private readonly IQueueProducer<CreatePipelineExecutionRequest> _createInstanceProducer;
        private readonly IQueueProducer<PipelineStartCommandRequest> _pipelineStartCommandProducer;
        private readonly IQueueProducer<GetPipelineExecutionStatusRequest> _getPipelineExecutionStatusProducer;
        private readonly IQueueProducer<GetPipelineExecutionDateRequest> _executionDateProducer;

        private readonly IQueueProducer<SetPipelineExecutionDateRequest> _setExecutionDateProducer;


        public PipelineService(
            ILogger<PipelineService> logger,
            ITicketService ticketService,
            IQueueProducer<GetPipelinesRequest> getPipelinesRequestProducer,
            IQueueProducer<CreatePipelineExecutionRequest> createInstanceProducer,
            IQueueProducer<PipelineStartCommandRequest> pipelineStartCommandProducer,
            IQueueProducer<GetPipelineExecutionStatusRequest> getPipelineExecutionStatusProducer,
            IQueueProducer<GetPipelineExecutionDateRequest> executionDateProducer,
            IQueueProducer<SetPipelineExecutionDateRequest> setexecutionDateProducer)
        {
            _logger = logger;
            _ticketService = ticketService;
            _getPipelinesRequestProducer = getPipelinesRequestProducer;
            _createInstanceProducer = createInstanceProducer;
            _pipelineStartCommandProducer = pipelineStartCommandProducer;
            _getPipelineExecutionStatusProducer = getPipelineExecutionStatusProducer;
            _executionDateProducer = executionDateProducer;
            _setExecutionDateProducer = setexecutionDateProducer;
        }

        public Guid CreatePipelineExecution(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new CreatePipelineExecutionRequest()
            {
                TicketId = ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),

                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                PipelineId = pipelineId
            };

            _createInstanceProducer.PublishMessage(message);
            _logger.LogDebug("CreatePipelineExecutionRequest Enqueued");

            return ticketId;
        }

        public Guid GetExecutionStatus(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetPipelineExecutionStatusRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                ExecutionId = executionId
            };

            _getPipelineExecutionStatusProducer.PublishMessage(message);

            _logger.LogDebug("GetPipelineExecutionStatus Enqueued");

            return ticketId;
        }

        public Guid GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetPipelinesRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                PipelineId = pipelineId
            };

            _getPipelinesRequestProducer.PublishMessage(message);

            _logger.LogDebug("GetPipelinesRequest Enqueued");

            return ticketId;
        }

        public Guid PostStartCommand(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new PipelineStartCommandRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                PipelineId = pipelineId,
                ExecutionId = executionId
            };

            _pipelineStartCommandProducer.PublishMessage(message);

            _logger.LogDebug("PipelineStartCommandRequest Enqueued");

            return ticketId;
        }

        // Authors: s242147 and s241747 : Method to request the execution dates of a pipeline
        public Guid RequestPipelineExecutionDate(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            // Create a new ticket for tracking the request
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            // Create the message for requesting the pipeline execution date
            var message = new GetPipelineExecutionDateRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                PipelineId = pipelineId,
                RepoId = repositoryId,
                OrganizationId = organizationId
            };

            // Publish the message to RabbitMQ
            _executionDateProducer.PublishMessage(message);

            // Log that the request has been enqueued
            _logger.LogDebug("GetPipelineExecutionDateRequest Enqueued");

            return ticketId;
        }

        // Authors: s242147 and s241747 : Method to set the execution date of a pipeline
        public Guid SetPipelineExecutionDate(Guid organizationId, Guid repositoryId, Guid pipelineId, String executionDate)
        {
            // Create a new ticket for tracking the request
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            // Create the message for setting the pipeline execution date
            var message = new SetPipelineExecutionDateRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                PipelineId = pipelineId,
                ExecutionDate = executionDate,
                RepositoryId = repositoryId,
                OrganizationId = organizationId
            };

            // Publish the message to RabbitMQ
            _setExecutionDateProducer.PublishMessage(message);

            // Log that the request has been enqueued
            _logger.LogDebug("SetPipelineExecutionDateRequest Enqueued");

            return ticketId;
        }

    }
}
