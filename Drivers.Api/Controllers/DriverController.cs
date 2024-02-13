using Microsoft.AspNetCore.Mvc;
using Drivers.Api.Models;
using Drivers.Api.Services;
using System.Threading.Tasks;
using dotnet_test.Models;
using Drivers.Api.DTO;

namespace Drivers.Api.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<DriverResponse>>>> GetAll()
        {
            return Ok(await _driverService.GetAllDriversAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<DriverResponse>>> Get(string id)
        {
            var response = await _driverService.GetDriverByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<DriverResponse>>>> Post([FromBody] DriverRequest driverRequest)
        {
            var response = await _driverService.AddDriverAsync(driverRequest);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<DriverResponse>>> Put(string id, [FromBody] DriverRequest driverRequest)
        {
            var response = await _driverService.UpdateDriverAsync(id, driverRequest);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<DriverResponse>>>> Delete(string id)
        {
            var response = await _driverService.DeleteDriverAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
