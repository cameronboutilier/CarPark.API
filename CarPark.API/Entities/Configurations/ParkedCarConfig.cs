using CarPark.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPark.API.Entities.Configurations;

public class ParkedCarConfig : IEntityTypeConfiguration<ParkedCar>
{
    public void Configure(EntityTypeBuilder<ParkedCar> builder)
    {
        builder.ToTable("ParkedCar");
        builder.HasKey(x => x.ParkingId);
        builder.Property(x => x.ParkingId).UseSequence();
        builder.Property(x => x.VehicleReg).IsRequired();
        builder.Property(x => x.ParkingDate).IsRequired();
    }
}