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
        private readonly IQueueProducer<GetAllPipelineResultsRequest> _getAllPipelineResultsProducer;
        private readonly IQueueProducer<GetPipelineResultByIdRequest> _getPipelineResultByIdProducer;
        private readonly IQueueProducer<GetPipelineResultByExecutionIdRequest> _getPipelineResultByExecutionIdProducer;

        public PipelineResultsService(
            ILogger<PipelineResultsService> logger,
            ITicketService ticketService,
            IQueueProducer<GetAllPipelineResultsRequest> getAllPipelineResultsProducer,
            IQueueProducer<GetPipelineResultByIdRequest> getPipelineResultByIdProducer,
            IQueueProducer<GetPipelineResultByExecutionIdRequest> getPipelineResultByExecutionIdProducer)
        {
            _logger = logger;
            _ticketService = ticketService;
            _getAllPipelineResultsProducer = getAllPipelineResultsProducer;
            _getPipelineResultByIdProducer = getPipelineResultByIdProducer;
            _getPipelineResultByExecutionIdProducer = getPipelineResultByExecutionIdProducer;
        }

        public Guid GetAllPipelineResultsAsync()
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetAllPipelineResultsRequest
            {
                TicketId = ticketId,
                TimeToLive = TimeSpan.FromMinutes(1)
            };

            _getAllPipelineResultsProducer.PublishMessage(message);
            _logger.LogDebug("GetAllPipelineResultsRequest Enqueued");

            return ticketId;
        }

        public Guid GetPipelineResultByIdAsync(string id)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetPipelineResultByIdRequest
            {
                TicketId = ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                ResultId = id
            };

            _getPipelineResultByIdProducer.PublishMessage(message);
            _logger.LogDebug("GetPipelineResultByIdRequest Enqueued");

            return ticketId;
        }

        public Guid GetPipelineResultByExecutionIdAsync(string executionId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetPipelineResultByExecutionIdRequest
            {
                TicketId = ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                ExecutionId = executionId
            };

            _getPipelineResultByExecutionIdProducer.PublishMessage(message);
            _logger.LogDebug("GetPipelineResultByExecutionIdRequest Enqueued");

            return ticketId;
        }
    }
}