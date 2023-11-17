using ErpSystem.TruckData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.TruckData.Infrastructure
{
    public class TruckDataDbContext : DbContext
    {
        public TruckDataDbContext(DbContextOptions<TruckDataDbContext> options)
            : base(options)
        { }

        public DbSet<Truck> Trucks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TruckEntityTypeConfiguration());
        }
    }
}
