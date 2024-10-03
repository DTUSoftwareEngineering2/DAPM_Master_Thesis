using System.Collections.Generic;
using DAPM.PipelineOrchestratorMS.Api.Models;

namespace DAPM.PipelineOrchestratorMS.Api.Services.Interfaces
{
    public interface IPipelineResultsService
    {
        List<PipelineExecutionStatus> GetResultsByPipelineId(int pipelineId);
        PipelineExecutionStatus GetResultById(int pipelineId, int resultId);
    }
}
