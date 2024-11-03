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
    [EnableCors("AllowAll")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;


        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly ITicketService _ticketService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService, ITicketService ticketService, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _authService = authService;
            _ticketService = ticketService;
        }

        [HttpPost("login")]
        [SwaggerOperation(Description = "Login a user")]
        public IActionResult Login([FromBody] LoginForm loginRequest)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token

            var tId = _authService.GetUserByMail(loginRequest.Email);
            JToken resolutionJSON = _ticketService.GetTicketResolution(tId);

            while ((int)resolutionJSON["status"] != 1)
            {
                resolutionJSON = _ticketService.GetTicketResolution(tId);
            }

            if (resolutionJSON["result"]["user"].ToString() == "not found")
            {
                return StatusCode(400, "User with the specified mail does not exists");
            }

            var hashPassword = resolutionJSON["result"]["user"]["hashPassword"].ToString();
            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, hashPassword))
            {
                return Unauthorized("The password and username does not match");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,
                        resolutionJSON["result"]["user"]["id"].ToString()),  // Subject claim (email or user ID)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique token ID claim
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,  // Include the claims in the token
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );


            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(new { AccessToken = token, userId = resolutionJSON["result"]["user"]["id"].ToString() });
        }

        [HttpPost("signup")]
        [EnableCors("AllowAll")]
        [SwaggerOperation(Description = "Signup a new user")]
        public IActionResult Signup([FromBody] SignupForm signupRequest)
        {
            var userId = Guid.NewGuid();

            _authService.PostUserToRepository(
                    userId,
                    signupRequest.FirstName,
                    signupRequest.LastName,
                    signupRequest.Email,
                    Guid.NewGuid(),
                    BCrypt.Net.BCrypt.HashPassword(signupRequest.Password)
                    );

            return Ok(new { userId = userId });
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;


        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _authService;

        public UserController(ILogger<UserController> logger, IAuthService authService, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _authService = authService;
        }

        [HttpGet("info")]
        [Authorize]
        [SwaggerOperation(Description = "Get the information of the currently logged in user")]
        public IActionResult getUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var tId = _authService.GetUserById(Guid.Parse(userId), false);

            return Ok(new { ticketId = tId });
        }

    }

}
