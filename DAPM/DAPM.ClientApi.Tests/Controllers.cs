using Moq;
using DAPM.ClientApi.Services.Interfaces;
using DAPM.ClientApi.Controllers;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

public class ClientsControllerTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<ITicketService> _mockTicketService;
    private readonly AuthController _controller;
    private Mock<ILogger<AuthController>> _logger;
    private Mock<IConfiguration> _config;

    public ClientsControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockTicketService = new Mock<ITicketService>();
        _logger = new Mock<ILogger<AuthController>>();
        _config = new Mock<IConfiguration>();
        _controller = new AuthController(_logger.Object, _mockAuthService.Object, _mockTicketService.Object, _config.Object);
    }

    [Fact]
    public async Task GetClientById_ShouldReturnClient_WhenClientExists()
    {
        // var t = _controller.Post(new LoginForm() { Email = "admin@exampl.ch", Password = "a" });
        // Arrange
        Assert.Equal(0, 1);
    }
}

