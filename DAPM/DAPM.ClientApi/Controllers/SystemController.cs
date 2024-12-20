﻿using DAPM.ClientApi.Models;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DAPM.ClientApi.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("system")]
    public class SystemController : ControllerBase
    {
        private readonly ILogger<SystemController> _logger;
        private ISystemService _systemService;

        public SystemController(ILogger<SystemController> logger, ISystemService systemService)
        {
            _logger = logger;
            _systemService = systemService;
        }


        [HttpPost("collab-handshake")]
        [SwaggerOperation(Description = "Performs a collaboration handshake with another peer in the platform. The domain provided must be valid and needs to correspond to an " +
            "existing peer.")]
        public async Task<ActionResult<Guid>> StartCollabHandshake([FromBody] CollabHandshakeDto collabHandshakeDto)
        {
            Guid id = _systemService.StartCollabHandshake(collabHandshakeDto.TargetPeerDomain);
            return Ok(new ApiResponse { RequestName = "CollabHandshake", TicketId = id });
        }
    }
}
