namespace CarPark.API.Models.Entities;

/// <summary>
/// Basicc entity for a parked car
/// </summary>
public class ParkedCar
{
    public int ParkingId { get; set; }
    public required string VehicleReg { get; set; }
    public DateTime ParkingDate { get; set; }
    public ParkingSpace? ParkingSpace { get; set; }
}