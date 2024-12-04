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
    public class GetAllUserConsumer : IQueueConsumer<GetAllUserMessage>
    {
        private ILogger<GetAllUserConsumer> _logger;
        private IQueueProducer<GetAllUserResult> _getAllUserResultQueueProducer;
        private IUserService _userService;
        public GetAllUserConsumer(ILogger<GetAllUserConsumer> logger,
            IQueueProducer<GetAllUserResult> getAllUserResultQueueProducer,
            IUserService userService)
        {
            _logger = logger;
            _getAllUserResultQueueProducer = getAllUserResultQueueProducer;
            _userService = userService;
        }

        public async Task ConsumeAsync(GetAllUserMessage message)
        {
            _logger.LogInformation("GetAllUserMessage received");

            // var t = await _userService.GetUserByMail("test@gmail.com");
            List<User>? u;
            u = await _userService.GetAllUsers(message.managerId);

            List<UserDTO>? userDTO = null;
            if (u != null)
            {

                userDTO = new List<UserDTO>();

                for (int i = 0; i < u.Count; i++)
                {
                    userDTO.Add(new UserDTO
                    {
                        Id = u[i].Id,
                        FirstName = u[i].FirstName,
                        LastName = u[i].LastName,
                        Mail = u[i].Mail,
                        Organization = u[i].Organization,
                        UserRole = u[i].UserRole,
                        accepted = u[i].accepted,
                    });

                }
            }

            var resultMessage = new GetAllUserResult
            {
                MessageId = Guid.NewGuid(),
                TicketId = message.TicketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                users = userDTO
            };


            _getAllUserResultQueueProducer.PublishMessage(resultMessage);
            return;
        }
    }
}
