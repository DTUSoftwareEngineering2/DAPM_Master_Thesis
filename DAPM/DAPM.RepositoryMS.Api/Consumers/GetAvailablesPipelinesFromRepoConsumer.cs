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
    public class GetAvailablePipelinesFromRepoProducer : IQueueConsumer<GetAvailablePipelinesFromRepoMessage>
    {
        private ILogger<GetAvailablePipelinesFromRepoProducer> _logger;
        private IRepositoryService _repositoryService;
        IQueueProducer<GetAvailablesPipelinesFromRepoResultMessage> _getAvailablePipelinesFromRepoResultProducer;

        public GetAvailablePipelinesFromRepoProducer(ILogger<GetAvailablePipelinesFromRepoProducer> logger,
            IRepositoryService repositoryService,
            IQueueProducer<GetAvailablesPipelinesFromRepoResultMessage> getAvailablePipelinesFromRepoResultProducer)
        {
            _logger = logger;
            _repositoryService = repositoryService;
            _getAvailablePipelinesFromRepoResultProducer = getAvailablePipelinesFromRepoResultProducer;
        }

        public async Task ConsumeAsync(GetAvailablePipelinesFromRepoMessage message)
        {
            _logger.LogInformation("GetAvailablePipelinesFromRepoMessage received");

            List<Models.PostgreSQL.Pipeline>? pipelines = await _repositoryService.GetAllAvailablePipelines(message.RepositoryId);


            List<PipelineDTO>? resultPipelines = null;
            if (pipelines != null)
            {
                resultPipelines = new List<PipelineDTO>();
                for (int i = 0; i < pipelines.Count; i++)
                {
                    RabbitMQLibrary.Models.Pipeline pipeline = JsonConvert.DeserializeObject<RabbitMQLibrary.Models.Pipeline>(pipelines[i].PipelineJson);
                    pipeline.visibility = pipelines[i].visibility;
                    pipeline.userId = pipelines[i].userId;
                    var pipelineDTO = new PipelineDTO()
                    {
                        Id = pipelines[i].Id,
                        RepositoryId = pipelines[i].RepositoryId,
                        Name = pipelines[i].Name,
                        Pipeline = pipeline,
                    };
                    resultPipelines.Add(pipelineDTO);
                }



            }

            var resultMessage = new GetAvailablesPipelinesFromRepoResultMessage
            {
                ProcessId = message.ProcessId,
                MessageId = message.MessageId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Pipelines = resultPipelines
            };

            _getAvailablePipelinesFromRepoResultProducer.PublishMessage(resultMessage);

            _logger.LogInformation("GetAvailablePipelinesFromRepoMessage produced");

            return;
        }
    }
}
