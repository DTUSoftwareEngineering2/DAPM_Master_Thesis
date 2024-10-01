using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    [Route("api2/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loginRequest.Email),  // Subject claim (email or user ID)
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

            return Ok(token);
        }

        [HttpGet("secure")]
        [Authorize]
        public IActionResult Get()
        {
            return Ok($"This is a secure endpoint");
        }

    }




}
