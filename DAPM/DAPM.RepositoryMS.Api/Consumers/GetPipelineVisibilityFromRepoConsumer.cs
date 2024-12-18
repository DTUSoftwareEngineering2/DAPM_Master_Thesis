using DAPM.RepositoryMS.Api.Models.PostgreSQL;
using DAPM.RepositoryMS.Api.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

namespace DAPM.RepositoryMS.Api.Consumers
{
    // Author: Maxime Rochat - s241741
    public class GetPipelineVisibilityFromRepoConsumer : IQueueConsumer<GetPipelineVisibilityFromRepoMessage>
    {
        private ILogger<GetPipelineVisibilityFromRepoConsumer> _logger;
        private IRepositoryService _repositoryService;
        private IPipelineService _pipelineService;
        IQueueProducer<GetPipelineVisibilityFromRepoResult> _getPipelineVisibilityFromRepoResultQueue;

        public GetPipelineVisibilityFromRepoConsumer(ILogger<GetPipelineVisibilityFromRepoConsumer> logger,
            IRepositoryService repositoryService,
            IPipelineService pipelineService,
            IQueueProducer<GetPipelineVisibilityFromRepoResult> getPipelineVisibilityFromRepoResultQueue)
        {
            _logger = logger;
            _repositoryService = repositoryService;
            _pipelineService = pipelineService;
            _getPipelineVisibilityFromRepoResultQueue = getPipelineVisibilityFromRepoResultQueue;
        }

        public async Task ConsumeAsync(GetPipelineVisibilityFromRepoMessage message)
        {
            _logger.LogInformation("GetPipelineVisibilityFromRepoMessage received");

            Models.PostgreSQL.Pipeline? pipeline = await _pipelineService.GetPipelineById(message.RepositoryId, message.PipelineId);
            int vis = pipeline == null ? 0 : pipeline.visibility;
            var resultMessage = new GetPipelineVisibilityFromRepoResult
            {
                ProcessId = message.ProcessId,
                MessageId = message.MessageId,
                TimeToLive = TimeSpan.FromMinutes(1),
                PipelineId = message.PipelineId,
                visibility = vis,
            };

            _getPipelineVisibilityFromRepoResultQueue.PublishMessage(resultMessage);

            _logger.LogInformation("GetPipelineVisibilityFromRepoMessage produced");

            return;
        }
    }
}
