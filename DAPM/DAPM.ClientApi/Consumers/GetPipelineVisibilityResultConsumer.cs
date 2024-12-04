using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetPipelineVisibilityResultConsumer : IQueueConsumer<GetPipelineVisibilityResult>
    {
        private ILogger<GetPipelineVisibilityResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetPipelineVisibilityResultConsumer(ILogger<GetPipelineVisibilityResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetPipelineVisibilityResult message)
        {
            _logger.LogInformation("GetPipelineVisibilityResult received");

            JToken result = new JObject();

            result["pipelineId"] = message.pipelineId;
            result["visibility"] = message.visbility;

            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
