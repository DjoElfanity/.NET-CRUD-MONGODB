using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_test.Models;
using Drivers.Api.DTO;
using Drivers.Api.Models;

namespace Drivers.Api.Services
{
    public interface IDriverService
    {
        Task<ServiceResponse<List<DriverResponse>>> GetAllDriversAsync();
        Task<ServiceResponse<DriverResponse>> GetDriverByIdAsync(string id);
        Task<ServiceResponse<List<DriverResponse>>> AddDriverAsync(DriverRequest driverRequest);
        Task<ServiceResponse<DriverResponse>> UpdateDriverAsync(string id, DriverRequest driverRequest);
        Task<ServiceResponse<List<DriverResponse>>> DeleteDriverAsync(string id);
    }
}
