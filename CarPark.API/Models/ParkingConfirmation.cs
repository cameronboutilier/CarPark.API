namespace CarPark.API.Models;

public class ParkingConfirmation
{
    public required string VehicleReg { get; set; }
    public int SpaceNumber { get; set; }
    public DateTime TimeIn { get; set; }
}