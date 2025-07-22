using CarPark.API.Contracts;
using CarPark.API.Models.Configuration;
using CarPark.API.Models.Entities;
using CarPark.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;

namespace TestProject1;

public class Tests
{
    private readonly Mock<IParkingRepository> mockParkingRepo = new ();
    private Mock<IOptions<ParkingCostConfig>> mockOptions = new ();
    private IParkingService parkingService;
        
    [SetUp]
    public void Setup()
    {
        parkingService = new ParkingService(mockParkingRepo.Object, mockOptions.Object);
        
        //Result when get parked car exists
        mockParkingRepo.Setup(repository => repository.GetParkedCar(It.Is<string>(s => s == "ExistingCar")))
            .Returns(new ParkedCar
            {
                ParkingId = 1,
                ParkingDate = new DateTime(2025,7,22,14,0,0),
                VehicleReg = "ExistingCar",
                ParkingSpace = new ParkingSpace()
                {
                    ParkingSpaceId = 1,
                    ParkingId = null
                }
            });
        
        //Result when parked car does not exist
        mockParkingRepo.Setup(repository => repository.GetParkedCar(It.Is<string>(s => s == "NonExistingCar")))
            .Returns((ParkedCar?)null);
        
        //Result when all parking spaces are full
        mockParkingRepo.Setup(repository => repository.ParkCar(It.Is<string>(s => s == "OverflowCar")))
            .Returns((ParkingSpace?)null);

        //Result when valid park is called
        mockParkingRepo.Setup(repository => repository.ParkCar(It.Is<string>(s => s == "NonExistingCar")))
            .Returns(new ParkingSpace()
            {
                ParkingSpaceId = 2,
                ParkingId = 2,
                ParkedCar = new ParkedCar()
                {
                    ParkingId = 2,
                    ParkingDate = new DateTime(2025,7,22,14,0,0),
                    VehicleReg = "NonExistingCar",
                }
            });
    }

    //Case 1: Vehicle is already parked
    [Test]
    public void TestParkCarAlreadyParked()
    {
        var ex = Assert.Throws<BadHttpRequestException>(() => parkingService.ParkCar("ExistingCar"));
        Assert.That(ex.Message.Equals("This vehicle is already parked."));
    }

    //Case 2: There are no parking spots available
    [Test]
    public void TestParkCarNoSpacesAvailable()
    {
        var ex = Assert.Throws<BadHttpRequestException>(() => parkingService.ParkCar("OverflowCar"));
        Assert.That(ex.Message.Equals("No available spaces found."));
    }

    //Case 3: Vehicle is parked successfully
    [Test]
    public void TestParkCarSuccess()
    {
        var parkedCar = parkingService.ParkCar("NonExistingCar");
        Assert.NotNull(parkedCar);
        Assert.That(parkedCar.VehicleReg.Equals("NonExistingCar"));
        Assert.That(parkedCar.TimeIn.Equals(new DateTime(2025,7,22,14,0,0)));
    }
}