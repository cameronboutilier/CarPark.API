using CarPark.API.Models;

namespace CarPark.API.Contracts;

public interface IParkingService
{
    ParkingConfirmation ParkCar(string vehicleReg);
    ParkingExitConfirmation ExitCar(string vehicleReg);
    ParkingAvailability GetAvailability();
}