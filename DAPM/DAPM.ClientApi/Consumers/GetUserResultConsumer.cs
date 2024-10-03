using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetUserResultConsumer : IQueueConsumer<GetUserResult>
    {
        private ILogger<GetUserResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetUserResultConsumer(ILogger<GetUserResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetUserResult message)
        {
            _logger.LogInformation("GetUserResult received");


            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            JToken idsJSON = JToken.FromObject(message.user, serializer);

            //Serialization
            result["user"] = idsJSON;
            result["test_res"] = "Passed the whole processing";
            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
