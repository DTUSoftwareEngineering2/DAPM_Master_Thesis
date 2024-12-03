using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class SetPipelineExecutionDateResultConsumer : IQueueConsumer<SetPipelineExecutionDateResultMessage>
    {
        private readonly ILogger<SetPipelineExecutionDateResultConsumer> _logger;
        private readonly ITicketService _ticketService;

        public SetPipelineExecutionDateResultConsumer(ILogger<SetPipelineExecutionDateResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(SetPipelineExecutionDateResultMessage message)
        {
            _logger.LogInformation("SetPipelineExecutionDateResultMessage received for PipelineId: {PipelineId}", message.PipelineId);

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            // Serialization
            result["success"] = message.Success;
            result["message"] = message.Message;

            // Update resolution with the success status and additional info
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}