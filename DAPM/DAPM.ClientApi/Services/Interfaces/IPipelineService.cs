using RabbitMQLibrary.Messages.ClientApi;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IPipelineService
    {
        public Guid GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId);
        public Guid CreatePipelineExecution(Guid organizationId, Guid repositoryId, Guid pipelineId);
        public Guid PostStartCommand(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId);
        public Guid GetExecutionStatus(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId);

        // Authors: s242147 and s241747 : Interface for the GetPipelineExecution
        public Guid RequestPipelineExecutionDate(Guid organizationId, Guid repositoryId, Guid pipelineId);

        // Authors: s242147 and s241747 : Interface for the SetPipelineExecution
        public Guid SetPipelineExecutionDate(Guid organizationId, Guid repositoryId, Guid pipelineId, String executionDate);

 
 

    }
}
