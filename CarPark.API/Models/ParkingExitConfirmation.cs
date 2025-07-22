namespace CarPark.API.Models;

public class ParkingExitConfirmation
{
    public required string VehicleReg { get; set; }
    public double VehicleCharge { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime TimeOut { get; set; }
}