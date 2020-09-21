using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using TVAEnergyData.EIAClient;
using TVAEnergyData.EIAClient.Models;
using TVAEnergyData.Web.Controllers;
using Xunit;

namespace TVAEnergyData.Web.Tests.ControllerTests
{
    public class EIASeriesControllerTests
    {
        [Fact]
        public async Task GetSeries_WithSeriesId_ReturnsSeriesInstanceAsync()
        {
            // Arrange
            var jsonString = await File.ReadAllTextAsync(Path.Join("TestFiles", "EIAResponse.json"));
            var eiaResponseObject = EIAResponse.FromJson(jsonString);

            var mockClient = new Mock<IEIARestClient>();
            mockClient.Setup(client => client.GetSeries(It.IsAny<string>(), null, null))
                .ReturnsAsync(eiaResponseObject);
            var controller = new EIASeriesController(mockClient.Object);

            // Act
            var result = await controller.GetSeries("EBA.TVA-ALL.D.H");

            // Assert
            Assert.Equal("EBA.TVA-ALL.D.H", result.SeriesId);
            Assert.True(!string.IsNullOrWhiteSpace(result.Name));
            Assert.True(!string.IsNullOrWhiteSpace(result.Description));
            Assert.Equal("megawatthours", result.Units);
            Assert.True(result.Start > DateTime.MinValue);
            Assert.True(result.End > DateTime.MinValue);
            Assert.True(result.Updated > DateTime.MinValue);
            Assert.Equal(19, result.Samples.Count);
            Assert.True(result.Samples[0].Time > DateTime.MinValue);
            Assert.True(result.Samples[0].Value > 0);
        }
    }
}
