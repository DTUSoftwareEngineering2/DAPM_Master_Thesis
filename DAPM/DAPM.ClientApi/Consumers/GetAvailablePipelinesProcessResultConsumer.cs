using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetAvailablePipelinesProcessResultConsumer : IQueueConsumer<GetAvailablePipelinesProcessResult>
    {
        private ILogger<GetAvailablePipelinesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetAvailablePipelinesProcessResultConsumer(ILogger<GetAvailablePipelinesProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetAvailablePipelinesProcessResult message)
        {
            _logger.LogInformation("GetAvailablePipelinesProcessResult received");

            JToken result = new JObject();
            if (message.pipelines != null)
            {
                List<PipelineDTO> pipelinesDTOs = message.pipelines;
                // Objects used for serialization
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


                //Serialization
                JToken pipelinesJSON = JToken.FromObject(pipelinesDTOs, serializer);
                result["pipelines"] = pipelinesJSON;
            }
            else
            {
                result["pipelines"] = "";
            }


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
