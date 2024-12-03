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
    public class UpdateAcceptStatusConsumer : IQueueConsumer<UpdateAcceptStatusMessage>
    {
        private ILogger<UpdateAcceptStatusConsumer> _logger;
        private IQueueProducer<GetUserResult> _getUserResultQueueProducer;
        private IUserService _userService;
        public UpdateAcceptStatusConsumer(ILogger<UpdateAcceptStatusConsumer> logger,
            IQueueProducer<GetUserResult> getUserResultQueueProducer,
            IUserService userService)
        {
            _logger = logger;
            _getUserResultQueueProducer = getUserResultQueueProducer;
            _userService = userService;
        }

        public async Task ConsumeAsync(UpdateAcceptStatusMessage message)
        {
            _logger.LogInformation("UpdateAcceptStatusMessage received");

            // var t = await _userService.GetUserByMail("test@gmail.com");
            var u = await _userService.UpdateAcceptStatus(message.managerId, message.userId, message.accept);

            UserDTO? user = null;
            if (u != null)
            {

                user = new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Mail = u.Mail,
                    Organization = u.Organization,
                    UserRole = u.UserRole,
                    accepted = u.accepted,
                };

            }

            var resultMessage = new GetUserResult
            {
                MessageId = Guid.NewGuid(),
                TicketId = message.TicketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                user = user
            };


            _getUserResultQueueProducer.PublishMessage(resultMessage);
            return;
        }
    }
}
