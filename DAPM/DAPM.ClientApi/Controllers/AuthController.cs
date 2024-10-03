using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace DAPM.ClientApi.Controllers
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Organization { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }

    public class SignupModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Organization { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();
        private readonly List<User> _users = new List<User>(); // In-memory user storage (replace with database in production)

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupModel model)
        {
            if (_users.Any(u => u.Email == model.Email))
            {
                return BadRequest("User with this email already exists");
            }

            var user = new User
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Organization = model.Organization,
                PasswordHash = HashPassword(model.Password)
            };

            _users.Add(user);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = _users.FirstOrDefault(u => u.Email == login.Email);
            if (user != null && VerifyPassword(login.Password, user.PasswordHash))
            {
                var tokenResponse = GenerateTokens(user.Email);
                return Ok(tokenResponse);
            }
            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenModel model)
        {
            if (string.IsNullOrEmpty(model.RefreshToken))
            {
                return BadRequest("Refresh token is required");
            }

            if (!_refreshTokens.TryGetValue(model.RefreshToken, out var email))
            {
                return Unauthorized("Invalid refresh token");
            }

            var tokenResponse = GenerateTokens(email);
            _refreshTokens.Remove(model.RefreshToken);

            return Ok(tokenResponse);
        }

        [HttpGet("secure")]
        [Authorize]
        public IActionResult Get()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok($"This is a secure endpoint. Hello, {userEmail}!");
        }

        private TokenResponse GenerateTokens(string email)
        {
            var accessToken = GenerateAccessToken(email);
            var refreshToken = GenerateRefreshToken();
            _refreshTokens[refreshToken] = email;

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateAccessToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                // Add more claims as needed
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
