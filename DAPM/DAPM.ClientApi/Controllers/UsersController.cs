using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DAPM.ClientApi.Services.Interfaces;
using DAPM.ClientApi.Models.DTOs;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;
using Swashbuckle.AspNetCore.Annotations;

namespace DAPM.ClientApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _config;


        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _userService;

        public UsersController(ILogger<UsersController> logger, IUsersService userService, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _userService = userService;
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult getUserInfo()
        {
            var managerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var tId = _userService.GetAllUsers(Guid.Parse(managerId));

            return Ok(new { ticketId = tId });
        }

        [HttpPost("validate")]
        [Authorize]
        public IActionResult validateUser([FromBody] ValidateForm validateForm)
        {
            var managerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (validateForm.accept > 1 || validateForm.accept < 0)
            {
                return BadRequest("The accept field of the request should be 0 or 1");
            }

            var tId = _userService.AcceptUser(Guid.Parse(managerId), validateForm.userId, validateForm.accept, validateForm.role);

            return Ok(new { ticketId = tId });
        }

        [HttpGet("delete/{userId}")]
        [Authorize]
        public IActionResult validateUser(Guid userId)
        {
            var managerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var tId = _userService.RemoveUser(Guid.Parse(managerId), userId);

            return Ok(new { ticketId = tId });
        }

    }
}
