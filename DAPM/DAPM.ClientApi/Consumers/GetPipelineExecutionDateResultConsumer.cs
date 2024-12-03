using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetPipelineExecutionDateResultConsumer : IQueueConsumer<GetPipelineExecutionDateResultMessage>
    {
        private ILogger<GetPipelineExecutionDateResultConsumer> _logger;
        private readonly ITicketService _ticketService;

        public GetPipelineExecutionDateResultConsumer(ILogger<GetPipelineExecutionDateResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetPipelineExecutionDateResultMessage message)
        {
            _logger.LogInformation("GetPipelineExecutionDateResultRequest received for PipelineId: {PipelineId}", message.PipelineId);

            var executionDate = message.ExecutionDate;

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            // Serialization
            JToken executionDateJson = JToken.FromObject(executionDate, serializer);
            result["executionDate"] = executionDateJson.ToString();

            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}