using System;
using System.Collections.Generic;
using System.Linq;
using DAPM.PipelineOrchestratorMS.Api.Models;
using DAPM.PipelineOrchestratorMS.Api.Services.Interfaces;
using DAPM.PipelineOrchestratorMS.Api.Engine;

namespace DAPM.PipelineOrchestratorMS.Api.Services
{
    public class PipelineResultsService : IPipelineResultsService
    {
        // In-memory storage for pipeline results using the existing PipelineExecutionStatus and StepStatus classes
        private static readonly List<PipelineExecutionStatus> _pipelineResults = new List<PipelineExecutionStatus>
        {
            new PipelineExecutionStatus
            {
                ExecutionTime = new TimeSpan(0, 2, 0),  // 2 minutes
                State = PipelineExecutionState.Completed,
                CurrentSteps = new List<StepStatus>
                {
                    new StepStatus { StepId = Guid.NewGuid(), ExecutionerPeer = Guid.NewGuid(), StepType = "DataProcessing", ExecutionTime = new TimeSpan(0, 0, 45) },
                    new StepStatus { StepId = Guid.NewGuid(), ExecutionerPeer = Guid.NewGuid(), StepType = "Validation", ExecutionTime = new TimeSpan(0, 1, 15) }
                }
            },
            new PipelineExecutionStatus
            {
                ExecutionTime = new TimeSpan(0, 5, 0),  // 5 minutes
                State = PipelineExecutionState.Faulted,
                CurrentSteps = new List<StepStatus>
                {
                    new StepStatus { StepId = Guid.NewGuid(), ExecutionerPeer = Guid.NewGuid(), StepType = "DataLoading", ExecutionTime = new TimeSpan(0, 2, 30) },
                    new StepStatus { StepId = Guid.NewGuid(), ExecutionerPeer = Guid.NewGuid(), StepType = "Validation", ExecutionTime = new TimeSpan(0, 2, 30) }
                }
            },
            new PipelineExecutionStatus
            {
                ExecutionTime = new TimeSpan(0, 2, 30),  // 2 minutes, 30 seconds
                State = PipelineExecutionState.Running,
                CurrentSteps = new List<StepStatus>
                {
                    new StepStatus { StepId = Guid.NewGuid(), ExecutionerPeer = Guid.NewGuid(), StepType = "Step Initialization", ExecutionTime = new TimeSpan(0, 0, 30) }
                }
            }
        };

        // Get all results for a specific pipeline ID
        public List<PipelineExecutionStatus> GetResultsByPipelineId(int pipelineId)
        {
            // For demonstration purposes, returning all results since PipelineId is not defined in the existing class
            return _pipelineResults;
        }

        // Get a specific result by pipeline ID and result ID (assuming result ID corresponds to the index in the list)
        public PipelineExecutionStatus GetResultById(int pipelineId, int resultId)
        {
            // Since there is no ResultId property in the current class, we'll use the index as a substitute for this example
            return _pipelineResults.ElementAtOrDefault(resultId - 1);  // Adjusting resultId to match list index (0-based)
        }
    }
}
