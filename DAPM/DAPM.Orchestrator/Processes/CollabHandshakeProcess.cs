﻿using DAPM.Orchestrator.Services;
using DAPM.Orchestrator.Services.Models;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPeerApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.PeerApi;
using RabbitMQLibrary.Messages.PeerApi.Handshake;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace DAPM.Orchestrator.Processes
{
    public class CollabHandshakeProcess : OrchestratorProcess
    {
        private ILogger<CollabHandshakeProcess> _logger;
        private Identity _localPeerIdentity;
        private Identity _requestedPeerIdentity;
        private IIdentityService _identityService;
        private string _requestedPeerDomain;
        private Guid _ticketId;
        public CollabHandshakeProcess(OrchestratorEngine engine, IServiceProvider serviceProvider, 
            Guid ticketId, Guid processId, string requestedPeerDomain)
            : base(engine, serviceProvider, processId)
        {
            _requestedPeerDomain = requestedPeerDomain;
            _identityService = serviceProvider.GetRequiredService<IIdentityService>();
            _localPeerIdentity = _identityService.GetIdentity();
            _logger = serviceProvider.GetRequiredService<ILogger<CollabHandshakeProcess>>();
            _ticketId = ticketId;
        }

        public override void StartProcess()
        {

            _logger.LogInformation("HANDSHAKE STARTED");
            var sendHandshakeRequestProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<SendHandshakeRequestMessage>>();


            var identityDto = new IdentityDTO()
            {
                Id = _localPeerIdentity.Id,
                Name = _localPeerIdentity.Name,
                Domain = _localPeerIdentity.Domain,
            };

            var message = new SendHandshakeRequestMessage()
            {
                SenderProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                SenderPeerIdentity = identityDto,
                RequestedPeerDomain = _requestedPeerDomain
            };

            sendHandshakeRequestProducer.PublishMessage(message);
        }

        public override void OnHandshakeRequestResponse(HandshakeRequestResponseMessage message)
        {
            _logger.LogInformation("HANDSHAKE REQUEST RESPONSE RECEIVED");
            if (message.IsRequestAccepted == false)
            {
                EndProcess();
                return;
            }
            _requestedPeerIdentity = new Identity()
            {
                Id = message.SenderPeerIdentity.Id,
                Name = message.SenderPeerIdentity.Name,
                Domain = message.SenderPeerIdentity.Domain,
            };

            var getEntriesFromOrgProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetEntriesFromOrgMessage>>();

            var getEntriesFromOrgMessage = new GetEntriesFromOrgMessage()
            {
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                OrganizationId = (Guid)_localPeerIdentity.Id
            };

            getEntriesFromOrgProducer.PublishMessage(getEntriesFromOrgMessage);
        }

        
        public override void OnGetEntriesFromOrgResult(GetEntriesFromOrgResult message)
        {
            var sendRegistryUpdateProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<SendRegistryUpdateMessage>>();
            var registryUpdate = new RegistryUpdateDTO()
            {
                Organizations = new List<OrganizationDTO>() { message.Organization },
                Repositories = message.Repositories,
                Resources = message.Resources,
                Pipelines = message.Pipelines,
            };

            var senderIdentityDto = new IdentityDTO()
            {
                Id = _localPeerIdentity.Id,
                Name = _localPeerIdentity.Name,
                Domain = _localPeerIdentity.Domain,
            };

            var sendRegistryUpdateMessage = new SendRegistryUpdateMessage()
            {
                SenderProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                SenderPeerIdentity = senderIdentityDto,
                TargetPeerDomain = _requestedPeerDomain,
                RegistryUpdate = registryUpdate,
                IsPartOfHandshake = true
                
            };

            sendRegistryUpdateProducer.PublishMessage(sendRegistryUpdateMessage);
        }

        public override void OnRegistryUpdate(RegistryUpdateMessage message)
        {

            _logger.LogInformation("REGISTRY UPDATE RECEIVED");
            var applyRegistryUpdateProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<ApplyRegistryUpdateMessage>>();
            

            var applyRegistryUpdateMessage = new ApplyRegistryUpdateMessage()
            {
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                RegistryUpdate = message.RegistryUpdate

            };

            applyRegistryUpdateProducer.PublishMessage(applyRegistryUpdateMessage);
        }

        public override void OnApplyRegistryUpdateResult(ApplyRegistryUpdateResult message)
        {
            var sendRegistryUpdateAckProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<SendRegistryUpdateAckMessage>>();

            var senderIdentityDto = new IdentityDTO()
            {
                Id = _localPeerIdentity.Id,
                Name = _localPeerIdentity.Name,
                Domain = _localPeerIdentity.Domain,
            };

            var sendRegistryUpdateAckMessage = new SendRegistryUpdateAckMessage()
            {
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                SenderPeerIdentity = senderIdentityDto,
                TargetPeerDomain = _requestedPeerDomain,
                RegistryUpdateAck = new RegistryUpdateAckDTO() { IsCompleted = true }

            };

            sendRegistryUpdateAckProducer.PublishMessage(sendRegistryUpdateAckMessage);
        }

        public override void OnRegistryUpdateAck(RegistryUpdateAckMessage message)
        {
            _logger.LogInformation("REGISTRY UPDATE ACK RECEIVED");
            var handshakeProcessResultProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<CollabHandshakeProcessResult>>();


            var collabHandshakeProcessResult = new CollabHandshakeProcessResult()
            {
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                RequestedPeerIdentity = message.PeerSenderIdentity,
                Succeeded = message.RegistryUpdateAck.IsCompleted,
                Message = "The handshake was successful"
               
            };

            handshakeProcessResultProducer.PublishMessage(collabHandshakeProcessResult);
        }
    }
}
