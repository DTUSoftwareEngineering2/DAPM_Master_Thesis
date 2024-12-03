using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.ResourceRegistry;


namespace DAPM.ClientApi.Services
{
    public class PipelineResultsService : IPipelineResultsService
    {
        private readonly ILogger<PipelineResultsService> _logger;
        private readonly ITicketService _ticketService;
        private IQueueProducer<GetResourceFilesRequest> _getAllPipelinesResultRequestProducer;
        public PipelineResultsService(
            ILogger<PipelineResultsService> logger,
            ITicketService ticketService,
            IQueueProducer<GetResourceFilesRequest> getAllPipelinesResultRequestProducer
        )
        {
            _logger = logger;
            _ticketService = ticketService;
            _getAllPipelinesResultRequestProducer = getAllPipelinesResultRequestProducer;
        }

        public Guid GetAllPipelineResultsAsync(Guid organizationId, Guid repositoryId, Guid resourceId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetResourceFilesRequest
            {
                TicketId = ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                ResourceId = resourceId
            };

            _getAllPipelinesResultRequestProducer.PublishMessage(message);
            _logger.LogDebug("GetAllPipelineResultsRequest Enqueued");

            return ticketId;
        }

        public Guid GetPipelineResultByIdAsync(Guid organizationId, Guid repositoryId, Guid resourceId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetResourceFilesRequest
            {
                TicketId = ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                ResourceId = resourceId
            };

            _getAllPipelinesResultRequestProducer.PublishMessage(message);
            _logger.LogDebug("GetPipelineResultByIdRequest Enqueued");

            return ticketId;
        }
    }
}