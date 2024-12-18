using DAPM.RepositoryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using Microsoft.Extensions.Logging;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.Repository;


// Authors: s242147 and s241747 : Consumer for the GetPipelineExecutionDateRequest
namespace DAPM.RepositoryMS.Api.Consumers
{
    public class GetPipelineExecutionDateConsumer : IQueueConsumer<GetPipelineExecutionDateRequest>
    {
        private readonly IPipelineService _pipelineService;
        private readonly ILogger<GetPipelineExecutionDateConsumer> _logger;
        private readonly IQueueProducer<GetPipelineExecutionDateResultMessage> _producer;

        public GetPipelineExecutionDateConsumer(IPipelineService pipelineService, ILogger<GetPipelineExecutionDateConsumer> logger, 
                                                IQueueProducer<GetPipelineExecutionDateResultMessage> producer)
        {
            _pipelineService = pipelineService;
            _logger = logger;
            _producer = producer;
        }

        public async Task ConsumeAsync(GetPipelineExecutionDateRequest message)
        {
            _logger.LogInformation("GetPipelineExecutionDateMessage received for PipelineId: {PipelineId}", message.PipelineId);

            var executionDate = await _pipelineService.GetPipelineExecutionDate(message.PipelineId, message.RepoId);

            var resultMessage = new GetPipelineExecutionDateResultMessage()
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                PipelineId = message.PipelineId,
                ExecutionDate = executionDate,
                ProcessId = message.ProcessId,
                TicketId = message.TicketId
            };

            _logger.LogInformation("Execution Date list count: {Count}", executionDate.Count);            
            _producer.PublishMessage(resultMessage);
            _logger.LogInformation("GetPipelineExecutionDateResultMessage Enqueued");
        }
    }
}