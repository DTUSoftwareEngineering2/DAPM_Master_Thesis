
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class ClientApiIntegrationTests : IClassFixture<WebApplicationFactory<DAPM.ClientApi.WebApp>>
{
    private readonly HttpClient _client;

    public ClientApiIntegrationTests(WebApplicationFactory<DAPM.ClientApi.WebApp> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetClientById_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/api/clients/1");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}

