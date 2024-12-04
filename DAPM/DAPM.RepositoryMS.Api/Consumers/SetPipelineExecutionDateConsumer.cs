using DAPM.RepositoryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.ClientApi;
using Microsoft.Extensions.Logging;
using DAPM.RepositoryMS.Api.Models.PostgreSQL;

namespace DAPM.RepositoryMS.Api.Consumers
{
    public class SetPipelineExecutionDateConsumer : IQueueConsumer<SetPipelineExecutionDateRequest>
    {
        private readonly ILogger<SetPipelineExecutionDateConsumer> _logger;
        private readonly IPipelineService _pipelineService;
        private readonly IQueueProducer<SetPipelineExecutionDateResultMessage> _producer;

        public SetPipelineExecutionDateConsumer(ILogger<SetPipelineExecutionDateConsumer> logger, 
                                                IPipelineService pipelineService,
                                                IQueueProducer<SetPipelineExecutionDateResultMessage> producer)
        {
            _logger = logger;
            _pipelineService = pipelineService;
            _producer = producer;
        }

        public async Task ConsumeAsync(SetPipelineExecutionDateRequest message)
        {
            _logger.LogInformation("SetPipelineExecutionDateRequest received for PipelineId: {PipelineId}", message.PipelineId);

            try
            {
                await _pipelineService.SetPipelineExecutionDate(message.PipelineId, message.ExecutionDate, message.RepositoryId);

                // If successful, create a success result message
                var resultMessage = new SetPipelineExecutionDateResultMessage
                {
                    TimeToLive = TimeSpan.FromMinutes(1),
                    PipelineId = message.PipelineId,
                    TicketId = message.TicketId,
                    Success = true,
                    Message = "Execution date set successfully"
                };

                _producer.PublishMessage(resultMessage);
                _logger.LogInformation("SetPipelineExecutionDateResultMessage Enqueued for PipelineId: {PipelineId}", message.PipelineId);
            }
            catch (Exception ex)
            {
                // In case of error, send a failure message
                var resultMessage = new SetPipelineExecutionDateResultMessage
                {
                    TimeToLive = TimeSpan.FromMinutes(1),
                    PipelineId = message.PipelineId,
                    TicketId = message.TicketId,
                    Success = false,
                    Message = $"Failed to set execution date: {ex.Message}"
                };

                _producer.PublishMessage(resultMessage);
                _logger.LogError(ex, "Failed to set execution date for PipelineId: {PipelineId}", message.PipelineId);
            }
        }
    }
}