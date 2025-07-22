using CarPark.API.Contracts;
using CarPark.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarParkController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public CarParkController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        [Route("/parking")]
        public ActionResult<ParkingAvailability> Get()
        {
            try
            {
                return Ok(_parkingService.GetAvailability());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        [Route("/parking")]
        public ActionResult<ParkingConfirmation> Post([FromBody] string VehicleReg)
        {
            try
            {
                return Ok(_parkingService.ParkCar(VehicleReg));
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        [Route("/parking/exit")]
        public ActionResult<ParkingConfirmation> ExitVehicle([FromBody] string VehicleReg)
        {
            try
            {
                return Ok(_parkingService.ExitCar(VehicleReg));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
