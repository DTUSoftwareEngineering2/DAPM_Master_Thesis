using DAPM.RepositoryMS.Api.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

namespace DAPM.RepositoryMS.Api.Consumers
{
    public class PostPipelineDeleteToRepoMessageConsumer : IQueueConsumer<PostPipelineDeleteToRepoMessage>
    {
        private ILogger<PostPipelineDeleteToRepoMessageConsumer> _logger;
        private IRepositoryService _repositoryService;
        IQueueProducer<GetAvailablesPipelinesFromRepoResultMessage> _getAvailablesPipelinesFromRepoResultMessage;

        public PostPipelineDeleteToRepoMessageConsumer(ILogger<PostPipelineDeleteToRepoMessageConsumer> logger,
            IRepositoryService repositoryService,
            IQueueProducer<GetAvailablesPipelinesFromRepoResultMessage> getAvailablesPipelinesFromRepoResultMessage)
        {
            _logger = logger;
            _repositoryService = repositoryService;
            _getAvailablesPipelinesFromRepoResultMessage = getAvailablesPipelinesFromRepoResultMessage;
        }

        public async Task ConsumeAsync(PostPipelineDeleteToRepoMessage message)
        {
            _logger.LogInformation("PostPipelineDeleteToRepoMessage received");

            Models.PostgreSQL.Pipeline pipeline = await _repositoryService.DeletePipelineById(message.RepositoryId, message.PipelineId, message.UserId);


            List<PipelineDTO>? resultPipelines = null;
            if (pipeline != null)
            {
                resultPipelines = new List<PipelineDTO>();

                RabbitMQLibrary.Models.Pipeline pipelineModel = JsonConvert.DeserializeObject<RabbitMQLibrary.Models.Pipeline>(pipeline.PipelineJson);
                pipeline.visibility = pipeline.visibility;
                pipeline.userId = pipeline.userId;
                var pipelineDTO = new PipelineDTO()
                {
                    Id = pipeline.Id,
                    RepositoryId = pipeline.RepositoryId,
                    Name = pipeline.Name,
                    Pipeline = pipelineModel,
                };
                resultPipelines.Add(pipelineDTO);

            }

            var resultMessage = new GetAvailablesPipelinesFromRepoResultMessage
            {
                ProcessId = message.ProcessId,
                MessageId = message.MessageId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Pipelines = resultPipelines
            };

            _getAvailablesPipelinesFromRepoResultMessage.PublishMessage(resultMessage);

            _logger.LogInformation("GetAvailablePipelinesFromRepoMessage produced");



        }
    }
}
