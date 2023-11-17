using ErpSystem.TruckData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.TruckData.Infrastructure
{
    public class TruckEntityTypeConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.ToTable("Trucks", "TruckData");
            builder.HasKey(t => t.Id);

            //we can specify column names different than we have in class.
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Code).HasColumnName("Code");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.Description).HasColumnName("Description");

            builder.Property(t => t.Status).HasConversion<int>();

        }
    }
}
