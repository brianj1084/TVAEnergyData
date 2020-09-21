using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using TVAEnergyData.EIAClient.Models;
using Xunit;

namespace TVAEnergyData.EIAClient.Tests
{
    public class EIARestClientTests
    {
        [Fact]
        public async Task GetSeries_WithSeriesId_ReturnsValidResponse()
        {
            // Arrange
            var jsonString = await File.ReadAllTextAsync(Path.Join("TestFiles", "EIAResponse.json"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString),
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);

            var mockFactory = new Mock<IHttpClientFactory>();

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["EIASettings:BaseUrl"]).Returns("https://api.eia.gov/");
            configurationMock.Setup(c => c["EIASettings:APIKey"]).Returns("1234567890");

            var eiaClient = new EIARestClient(configurationMock.Object, mockFactory.Object);

            // Act
            var result = await eiaClient.GetSeries("EBA.TVA-ALL.D.H");

            // Assert
            Assert.Equal("EBA.TVA-ALL.D.H", result.Series[0].SeriesId);
        }

        [Fact]
        public async Task GetSeries_WithSeriesIdStartAndEnd_ReturnsValidResponse()
        {
            // Arrange
            var jsonString = await File.ReadAllTextAsync(Path.Join("TestFiles", "EIAResponse.json"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString),
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);

            var mockFactory = new Mock<IHttpClientFactory>();

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["EIASettings:BaseUrl"]).Returns("https://api.eia.gov/");
            configurationMock.Setup(c => c["EIASettings:APIKey"]).Returns("1234567890");

            var eiaClient = new EIARestClient(configurationMock.Object, mockFactory.Object);

            // Act
            var result = await eiaClient.GetSeries("EBA.TVA-ALL.D.H", "202000915", "20200916");

            // Assert
            Assert.Equal("EBA.TVA-ALL.D.H", result.Series[0].SeriesId);
        }
    }
}
