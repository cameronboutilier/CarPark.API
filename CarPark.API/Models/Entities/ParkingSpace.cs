namespace CarPark.API.Models.Entities;

/// <summary>
/// Basic parking space entity, can be expanded to have more
/// information about the parking space, location, size, etc.
/// </summary>
public class ParkingSpace
{
    public int ParkingSpaceId { get; init; }
    public int? ParkingId { get; init; }
    public ParkedCar? ParkedCar { get; init; }
}