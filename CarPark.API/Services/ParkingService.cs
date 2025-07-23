using CarPark.API.Contracts;
using CarPark.API.Models;
using CarPark.API.Models.Configuration;
using Microsoft.Extensions.Options;

namespace CarPark.API.Services;

/// <summary>
/// Parking service to contain all business logic and provide 
/// a layer between the data repository and controllers.
/// </summary>
public class ParkingService : IParkingService
{
    private readonly IParkingRepository _parkingRepository;
    private readonly ParkingCostConfig _costConfig;


    public ParkingService(IParkingRepository parkingRepository, IOptions<ParkingCostConfig> costConfig)
    {
        _parkingRepository = parkingRepository;
        _costConfig = costConfig.Value;
    }
    public ParkingConfirmation ParkCar(string vehicleReg)
    {
        if (_parkingRepository.GetParkedCar(vehicleReg) is not null)
        {
            throw new BadHttpRequestException("This vehicle is already parked.");
        }

        var parkingSpace = _parkingRepository.ParkCar(vehicleReg);

        if (parkingSpace is null)
        {
            throw new BadHttpRequestException("No available spaces found.");
        }
        
        return new ParkingConfirmation()
        {
            SpaceNumber = parkingSpace.ParkingSpaceId,
            VehicleReg = vehicleReg,
            TimeIn = parkingSpace.ParkedCar.ParkingDate
        };
    }

    public ParkingExitConfirmation ExitCar(string vehicleReg)
    {
        var car = _parkingRepository.RemoveParkedCar(vehicleReg);
        var exitTime = DateTime.UtcNow;
        
        if (car is null) throw new KeyNotFoundException("No car found for this vehicle registration.");
        
        double parkingCost = Math.Round((exitTime - car.ParkingDate).TotalMinutes * _costConfig.CostPerMinute, 2);

        return new ParkingExitConfirmation()
        {
            VehicleReg = vehicleReg,
            TimeIn = car.ParkingDate,
            TimeOut = exitTime,
            VehicleCharge = parkingCost
        };
    }

    public ParkingAvailability GetAvailability()
    {
        var spaces = _parkingRepository.GetParkingSpaces();
        
        return new ParkingAvailability()
        {
            AvailableSpaces = spaces.Count(x => x.ParkingId is null),
            OccupiedSpaces = spaces.Count(x => x.ParkingId is not null)
        };
    }
}