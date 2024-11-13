using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

public class ClientApiTests
{
    [Fact]
    public async Task GetClients_ShouldReturnValidResponse()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{ 'id': 1, 'name': 'Client1' }]"),
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://localhost")
        };

        // Act
        var response = await httpClient.GetAsync("/api/clients");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

