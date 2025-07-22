using CarPark.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPark.API.Entities.Configurations;

public class ParkingSpaceConfig : IEntityTypeConfiguration<ParkingSpace>
{
    public void Configure(EntityTypeBuilder<ParkingSpace> builder)
    {
        builder.ToTable("ParkingSpace");
        builder.HasKey(x => x.ParkingSpaceId);
        builder.Property(x => x.ParkingSpaceId);
        builder.Property(x => x.ParkingId);
        
        //Relations
        builder.HasOne(x => x.ParkedCar)
            .WithOne(x => x.ParkingSpace)
            .HasForeignKey<ParkingSpace>(x => x.ParkingId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}