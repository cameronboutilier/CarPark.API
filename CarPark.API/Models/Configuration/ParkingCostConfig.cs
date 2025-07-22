namespace CarPark.API.Models.Configuration;

/// <summary>
/// A simple configuration class set before host is built.
/// Allows for variable costs per minute, but defaults to £0.10.
/// </summary>
public class ParkingCostConfig
{
    public double CostPerMinute { get; set; } = 0.1d;
}