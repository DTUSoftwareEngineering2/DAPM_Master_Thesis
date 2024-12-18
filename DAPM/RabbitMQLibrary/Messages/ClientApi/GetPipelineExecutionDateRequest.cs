using RabbitMQLibrary.Interfaces;
using System;

// Authors: s242147 and s241747 : Message for requesting the execution dates of a pipeline
namespace RabbitMQLibrary.Messages.ClientApi
{
    public class GetPipelineExecutionDateRequest : IQueueMessage
    {
        public Guid MessageId { get; set; }       // Unique ID for tracking the message
        public Guid PipelineId { get; set; }      // ID of the pipeline for which execution date is requested
        public TimeSpan TimeToLive { get; set; }  // Optional Time to Live for the message
        public Guid RepoId { get; set; }  
        public Guid ProcessId { get; set; }       // Process ID to correlate with the original request   
        public Guid TicketId { get; set; }        // Ticket ID to correlate with the original request

        public Guid OrganizationId { get; set; }  // Organization ID to correlate with the original request
    }
}