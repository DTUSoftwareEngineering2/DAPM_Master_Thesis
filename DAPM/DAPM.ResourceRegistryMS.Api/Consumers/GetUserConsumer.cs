using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;


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
            var u = new UserDTO();
            /*foreach (var pipeline in pipelines)*/
            /*{*/
            /*    var r = new PipelineDTO*/
            /*    {*/
            /*        Id = pipeline.Id,*/
            /*        Name = pipeline.Name,*/
            /*        OrganizationId = pipeline.PeerId,*/
            /*        RepositoryId = pipeline.RepositoryId,*/
            /*    };*/
            /**/
            /*    pipelinesDTOs = pipelinesDTOs.Append(r);*/
            /*}*/
            /**/
            /*var resultMessage = new GetPipelinesResultMessage*/
            /*{*/
            /*    TimeToLive = TimeSpan.FromMinutes(1),*/
            /*    ProcessId = message.ProcessId,*/
            /*    Pipelines = pipelinesDTOs*/
            /*};*/

            var resultMessage = new GetUserResult
            {
                MessageId = Guid.NewGuid(),
                TicketId = message.TicketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                user = u,
            };

            _getUserResultQueueProducer.PublishMessage(resultMessage);

            return;
        }
    }
}
