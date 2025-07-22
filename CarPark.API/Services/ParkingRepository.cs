using CarPark.API.Contracts;
using CarPark.API.Entities;
using CarPark.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarPark.API.Services;

/// <summary>
/// Repository class to interface with database via entity framework
/// </summary>
public class ParkingRepository(ParkingDbContext dbContext) : IParkingRepository
{
    private DbSet<ParkedCar> ParkedCars => dbContext.ParkedCars;
    private DbSet<ParkingSpace> ParkingSpaces => dbContext.ParkingSpaces;

    /// <summary>
    /// Adds a parked car to the parked car table, and set the parking id
    /// on the first available ParkingSpace table. If no empty parking space
    /// is found, the operation is aborted and a null is returned. 
    /// </summary>
    /// <param name="vehicleReg">The vehicle registration string</param>
    /// <returns>The newly parked car, or null if there is no spaces available</returns>
    public ParkingSpace? ParkCar(string vehicleReg)
    {
        var space = ParkingSpaces.FirstOrDefault(x => x.ParkedCar == null);
        
        if(space == null) return null;
        
        var parkedCar = ParkedCars.Add(new ParkedCar()
        {
            VehicleReg = vehicleReg,
            ParkingDate = DateTime.UtcNow,
            ParkingSpace = space
        });
        
        dbContext.SaveChanges();
        
        return space;
    }
    
    /// <summary>
    /// Gets the parked car with the associated vehicle registration string,
    /// or null if it is not found.
    /// </summary>
    /// <param name="vehicleReg">The vehicle registration string</param>
    /// <returns>The parked car, or null if it's not found</returns>
    public ParkedCar? GetParkedCar(string vehicleReg)
    {
        return ParkedCars.FirstOrDefault(x => x.VehicleReg == vehicleReg);
    }

    /// <summary>
    /// Removes a parked car from the parked car table, the foreign key is
    /// set to null on the ParkingSpace table as part of the foreign key constraint.
    /// If no parked car is found with that registration a null is returned.
    /// </summary>
    /// <param name="vehicleReg">The vehicle registration string</param>
    /// <returns>The parked car that is exiting, or null if it's not found</returns>
    public ParkedCar? RemoveParkedCar(string vehicleReg)
    {
        var car = ParkedCars.FirstOrDefault(x => x.VehicleReg == vehicleReg);
        
        if (car == null) return null;
        
        ParkedCars.Remove(car);
        dbContext.SaveChanges();
        
        return car;
    }

    /// <summary>
    /// Simply returns a list of all the parking spaces with the
    /// associated parked cars.
    /// </summary>
    /// <returns>A list of parking spaces</returns>
    public IList<ParkingSpace> GetParkingSpaces()
    {
        return ParkingSpaces.Include(x => x.ParkedCar).ToList();
    }
}