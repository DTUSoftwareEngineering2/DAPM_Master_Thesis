using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly ILogger<UsersService> _logger;
        private readonly IQueueProducer<GetAllUserMessage> _getAllUserRequest;
        private readonly IQueueProducer<PostUserMessage> _postUserRequest;
        private readonly ITicketService _ticketService;

        public UsersService(ILogger<UsersService> logger,
            IQueueProducer<GetAllUserMessage> getAllUserRequest,
            IQueueProducer<PostUserMessage> postUserRequest,
            ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _getAllUserRequest = getAllUserRequest;
            _postUserRequest = postUserRequest;
        }

        public Guid GetAllUsers(Guid managerId)
        {
            var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetAllUserMessage
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                managerId = managerId,
                MessageId = Guid.NewGuid()
            };


            _getAllUserRequest.PublishMessage(message);

            _logger.LogDebug("GetAllUserMessage Enqueued");

            return ticketId;
        }

        public Guid AcceptUser(Guid ManagerId, Guid userId)
        {

            return Guid.Empty;
        }

        public Guid RemoveUser(Guid ManagerId, Guid userId)
        {
            return Guid.Empty;

        }


    }
}
