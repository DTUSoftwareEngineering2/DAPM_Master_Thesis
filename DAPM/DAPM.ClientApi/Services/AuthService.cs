using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IQueueProducer<GetUserMessage> _getUserRequest;
        private readonly IQueueProducer<PostUserMessage> _postUserRequest;
        private readonly ITicketService _ticketService;

        public AuthService(ILogger<AuthService> logger,
            IQueueProducer<GetUserMessage> getUserRequest,
            IQueueProducer<PostUserMessage> postUserRequest,
            ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _getUserRequest = getUserRequest;
            _postUserRequest = postUserRequest;
        }

        public Guid GetUserById(Guid id, Boolean needHash = true)
        {
            var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetUserMessage
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                userId = id,
                needHash = needHash,
                MessageId = Guid.NewGuid()
            };


            _getUserRequest.PublishMessage(message);

            _logger.LogDebug("GetOrganizationByIdMessage Enqueued");

            return ticketId;
        }

        public Guid GetUserByMail(String mail, bool needHash = true)
        {
            var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetUserMessage
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                mail = mail,
                needHash = needHash,
                MessageId = Guid.NewGuid()
            };


            _getUserRequest.PublishMessage(message);

            _logger.LogDebug("GetOrganizationByIdMessage Enqueued");

            return ticketId;
        }

        public void PostUserToRepository(Guid id, String firstName, String lastName, String mail, Guid org, String hashPassword)
        {

            var message = new PostUserMessage
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                MessageId = Guid.NewGuid(),
                user = new UserDTO()
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Mail = mail,
                    Organization = org,
                    HashPassword = hashPassword
                }
            };

            _postUserRequest.PublishMessage(message);

            _logger.LogDebug("PostUserToRepositoryEnqueued");

        }

        /*public Guid GetOrganizationById(Guid organizationId)*/
        /*{*/
        /*    var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);*/
        /**/
        /*    var message = new GetOrganizationsRequest*/
        /*    {*/
        /*        TimeToLive = TimeSpan.FromMinutes(1),*/
        /*        TicketId = ticketId,*/
        /*        OrganizationId = organizationId*/
        /*    };*/
        /**/
        /*    _getOrganizationsRequestProducer.PublishMessage(message);*/
        /**/
        /*    _logger.LogDebug("GetOrganizationByIdMessage Enqueued");*/
        /**/
        /*    return ticketId;*/
        /*}*/
        /**/
        /*public Guid GetOrganizations()*/
        /*{*/
        /**/
        /*    var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);*/
        /**/
        /*    var message = new GetOrganizationsRequest*/
        /*    {*/
        /*        TimeToLive = TimeSpan.FromMinutes(1),*/
        /*        TicketId = ticketId,*/
        /*        OrganizationId = null,*/
        /*    };*/
        /**/
        /*    _getOrganizationsRequestProducer.PublishMessage(message);*/
        /**/
        /*    _logger.LogDebug("GetOrganizationsMessage Enqueued");*/
        /**/
        /*    return ticketId;*/
        /**/
        /*}*/
        /**/
        /*public Guid GetRepositoriesOfOrganization(Guid organizationId)*/
        /*{*/
        /*    var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);*/
        /**/
        /*    var message = new GetRepositoriesRequest*/
        /*    {*/
        /*        TimeToLive = TimeSpan.FromMinutes(1),*/
        /*        TicketId = ticketId,*/
        /*        OrganizationId = organizationId,*/
        /*        RepositoryId = null,*/
        /*    };*/
        /**/
        /*    _getRepositoriesRequestProducer.PublishMessage(message);*/
        /**/
        /*    _logger.LogDebug("GetRepositoriesRequest Enqueued");*/
        /**/
        /*    return ticketId;*/
        /*}*/
        /**/
        /*public Guid PostRepositoryToOrganization(Guid organizationId, string name)*/
        /*{*/
        /*    var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);*/
        /**/
        /*    var message = new PostRepositoryRequest*/
        /*    {*/
        /*        TimeToLive = TimeSpan.FromMinutes(1),*/
        /*        TicketId = ticketId,*/
        /*        OrganizationId = organizationId,*/
        /*        Name = name,*/
        /*    };*/
        /**/
        /*    _postRepositoryRequestProducer.PublishMessage(message);*/
        /**/
        /*    _logger.LogDebug("PostRepositoryRequest Enqueued");*/
        /**/
        /*    return ticketId;*/
        /*}*/
    }
}
