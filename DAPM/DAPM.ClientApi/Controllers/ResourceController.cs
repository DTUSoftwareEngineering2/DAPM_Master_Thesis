﻿using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.ClientApi.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {

        private readonly ILogger<ResourceController> _logger;
        private readonly IResourceService _resourceService;

        public ResourceController(ILogger<ResourceController> logger, IResourceService resourceService)
        {
            _logger = logger;
            _resourceService = resourceService;
        }

        [HttpGet(Name = "resource")]
        public async Task<FileStreamResult> Get([FromQuery] string name)
        {
            throw new NotImplementedException();     
        }

        [HttpPost(Name = "resource")]
        public async Task<ActionResult> Post(ResourceForm resourceForm)
        {
            throw new NotImplementedException();
        }
    }
    
}