using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TVAEnergyData.Domain;
using Xunit;

namespace TVAEnergyData.Data.Tests
{
    public class TVAEnergyDataContextTests
    {
        [Fact]
        public void AddEIAPoint_WithInMemoryContext_SavesSuccessfully()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<TVAEnergyDataContext>()
                .UseInMemoryDatabase(databaseName: "TVAEnergyData")
                .Options;
            var context = new TVAEnergyDataContext(options);

            var point = new EIAPoint
            {
                SeriesId = "EBA.TVA-ALL.D.H",
                Name = "TVA_Load",
                Description = "TVA System Load",
                CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow
            };

            context.EIAPoints.Add(point);
            context.SaveChanges();

            var savedPoint = context.EIAPoints.First(p => p.SeriesId == "EBA.TVA-ALL.D.H");

            Assert.True(savedPoint.Id > 0);
            Assert.Equal(point.SeriesId, savedPoint.SeriesId);
            Assert.Equal(point.Name, savedPoint.Name);
            Assert.Equal(point.Description, savedPoint.Description);
            Assert.Equal(point.CreatedDateTime, savedPoint.CreatedDateTime);
            Assert.Equal(point.ModifiedDateTime, savedPoint.ModifiedDateTime);
        }
    }
}
