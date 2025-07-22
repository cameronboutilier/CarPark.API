using CarPark.API.Models.Entities;

namespace CarPark.API.Contracts;

public interface IParkingRepository
{
    IList<ParkingSpace> GetParkingSpaces();
    ParkingSpace? ParkCar(string vehicleReg);
    ParkedCar? RemoveParkedCar(string vehicleReg);
    ParkedCar? GetParkedCar(string vehicleReg);
}