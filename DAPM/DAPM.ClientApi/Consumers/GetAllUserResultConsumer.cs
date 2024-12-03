using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetAllUserResultConsumer : IQueueConsumer<GetAllUserResult>
    {
        private ILogger<GetAllUserResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetAllUserResultConsumer(ILogger<GetAllUserResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetAllUserResult message)
        {
            _logger.LogInformation("GetAllUserResult received");


            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            if (message.users != null)
            {
                JToken usersJson = JToken.FromObject(message.users, serializer);

                /*idsJSON["hashPassword"]?.Parent?.Remove();*/
                //Serialization
                result["users"] = usersJson;
            }
            else
            {
                result["user"] = "You do not have the right to list users";
            }

            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
