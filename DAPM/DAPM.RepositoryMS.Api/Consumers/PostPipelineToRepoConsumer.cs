using DAPM.RepositoryMS.Api.Models.PostgreSQL;
using DAPM.RepositoryMS.Api.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

namespace DAPM.RepositoryMS.Api.Consumers
{
    public class PostPipelineToRepoConsumer : IQueueConsumer<PostPipelineToRepoMessage>
    {
        private ILogger<PostPipelineToRepoConsumer> _logger;
        private IRepositoryService _repositoryService;
        private IPipelineService _pipelineService;
        IQueueProducer<PostPipelineToRepoResultMessage> _postPipelineToRepoResultProducer;

        public PostPipelineToRepoConsumer(ILogger<PostPipelineToRepoConsumer> logger,
            IRepositoryService repositoryService,
            IPipelineService pipelineService,
            IQueueProducer<PostPipelineToRepoResultMessage> postPipelineToRepoResultProducer)
        {
            _logger = logger;
            _repositoryService = repositoryService;
            _postPipelineToRepoResultProducer = postPipelineToRepoResultProducer;
            _pipelineService = pipelineService;
        }

        public async Task ConsumeAsync(PostPipelineToRepoMessage message)
        {
            _logger.LogInformation("PostPipelineToRepoMessage received");
            Models.PostgreSQL.Pipeline pipeline;
            if (message.pipelineId == null)
            {
                pipeline = await _repositoryService.CreateNewPipeline(message.RepositoryId, message.Name, message.Pipeline);
            }
            else
            {
                pipeline = await _pipelineService.GetPipelineById(message.RepositoryId, message.pipelineId.Value);
                if (pipeline == null)
                {
                    pipeline = await _repositoryService.CreateNewPipeline(message.RepositoryId, message.Name, message.Pipeline);
                }
                pipeline = await _pipelineService.ModifyPipelineById(message.RepositoryId, message.pipelineId.Value
                    , pipeline, message.Name);
            }


            if (pipeline != null)
            {
                var pipelineDTO = new PipelineDTO()
                {
                    Id = pipeline.Id,
                    RepositoryId = pipeline.RepositoryId,
                    Name = pipeline.Name,
                    Pipeline = JsonConvert.DeserializeObject<RabbitMQLibrary.Models.Pipeline>(pipeline.PipelineJson)
                };

                var resultMessage = new PostPipelineToRepoResultMessage
                {
                    ProcessId = message.ProcessId,
                    TimeToLive = TimeSpan.FromMinutes(1),
                    Message = "Item created successfully",
                    Succeeded = true,
                    Pipeline = pipelineDTO
                };

                _postPipelineToRepoResultProducer.PublishMessage(resultMessage);

                _logger.LogInformation("PostPipelineToRepoResultMessage produced");

            }
            else
            {
                _logger.LogInformation("There was an error creating the new pipeline");
            }

            return;
        }
    }
}
