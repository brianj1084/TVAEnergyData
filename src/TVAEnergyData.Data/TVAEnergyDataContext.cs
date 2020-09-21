using Microsoft.EntityFrameworkCore;
using TVAEnergyData.Domain;

namespace TVAEnergyData.Data
{
    public class TVAEnergyDataContext : DbContext
    {
        public TVAEnergyDataContext(DbContextOptions<TVAEnergyDataContext> options) : base(options)
        {
        }

        public DbSet<EIAPoint> EIAPoints { get; set; }
    }
}
