using CarPark.API.Entities.Configurations;
using CarPark.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarPark.API.Entities;

public class ParkingDbContext(DbContextOptions<ParkingDbContext> options) : DbContext(options)
{
    public DbSet<ParkedCar> ParkedCars { get; set; }
    public DbSet<ParkingSpace> ParkingSpaces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ParkedCarConfig());
        modelBuilder.ApplyConfiguration(new ParkingSpaceConfig());
    }
}