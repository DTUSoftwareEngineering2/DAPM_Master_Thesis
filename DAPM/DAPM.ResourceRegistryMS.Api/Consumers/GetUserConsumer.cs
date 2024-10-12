using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using DAPM.ResourceRegistryMS.Api.Models;

using RabbitMQLibrary.Messages.ClientApi;


using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models;

namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    public class GetUserConsumer : IQueueConsumer<GetUserMessage>
    {
        private ILogger<GetUserConsumer> _logger;
        private IQueueProducer<GetUserResult> _getUserResultQueueProducer;
        private IUserService _userService;
        public GetUserConsumer(ILogger<GetUserConsumer> logger,
            IQueueProducer<GetUserResult> getUserResultQueueProducer,
            IUserService userService)
        {
            _logger = logger;
            _getUserResultQueueProducer = getUserResultQueueProducer;
            _userService = userService;
        }

        public async Task ConsumeAsync(GetUserMessage message)
        {
            _logger.LogInformation("GetUserMessage received");

            // var t = await _userService.GetUserByMail("test@gmail.com");
            User? u;

            if (message.userId.HasValue)
            {
                u = await _userService.GetUserById(message.userId.Value);
            }
            else if (!string.IsNullOrEmpty(message.mail))
            {
                u = await _userService.GetUserByMail(message.mail);
            }
            else
            {
                u = null;

            }

            UserDTO? userDTO = null;
            if (u != null)
            {
                userDTO = new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Mail = u.Mail,
                    Organization = u.Organization,
                    HashPassword = message.needHash ? u.HashPassword : ""
                };
            }

            var resultMessage = new GetUserResult
            {
                MessageId = Guid.NewGuid(),
                TicketId = message.TicketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                user = userDTO
            };

            _getUserResultQueueProducer.PublishMessage(resultMessage);

            return;
        }
    }
}
