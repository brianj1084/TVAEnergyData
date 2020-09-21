using System.IO;
using TVAEnergyData.EIAClient.Models;
using Xunit;

namespace TVAEnergyData.EIAClient.Tests
{
    public class EIAResponseObjectTests
    {
        [Fact]
        public void EIAResponse_FromJson_WithValidJsonString_Success()
        {
            var jsonString = File.ReadAllText(Path.Join("TestFiles", "EIAResponse.json"));
            var eiaResponseObject = EIAResponse.FromJson(jsonString);

            Assert.Equal("EBA.TVA-ALL.D.H", eiaResponseObject.Request.SeriesId);
        }

        [Fact]
        public void EIAResponse_ToJson_WithValidInstance_Success()
        {
            var eiaResponseObject = new EIAResponse
            {
                Request = new Request {Command = "series", SeriesId = "EBA.TVA-ALL.D.H"},
                Series = new[]
                {
                    new Series
                    {
                        Name = "EBA.TVA-ALL.D.H",
                        Data = new[] {new[] { new Datum {String = "20200915T18Z" }, new Datum { Integer = 20000} }}
                    }
                }
            };

            var jsonString = eiaResponseObject.ToJson();

            Assert.NotEmpty(jsonString);
        }
    }
}
