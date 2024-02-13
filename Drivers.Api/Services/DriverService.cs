using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Drivers.Api.Models;
using Drivers.Api.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using dotnet_test.Models;
using Drivers.Api.DTO;

namespace Drivers.Api.Services
{
    public class DriverService : IDriverService
    {
        private readonly IMongoCollection<Driver> _driversCollection;
        private readonly IMapper _mapper;

        public DriverService(IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _driversCollection = mongoDb.GetCollection<Driver>(databaseSettings.Value.CollectionName);
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<ServiceResponse<List<DriverResponse>>> GetAllDriversAsync()
        {
            var drivers = await _driversCollection.Find(_ => true).ToListAsync();
            var driverResponses = _mapper.Map<List<DriverResponse>>(drivers);
            return new ServiceResponse<List<DriverResponse>> { Data = driverResponses };
        }

        public async Task<ServiceResponse<DriverResponse>> GetDriverByIdAsync(string id)
        {
            var driver = await _driversCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (driver == null)
            {
                return new ServiceResponse<DriverResponse> { Success = false, Message = "Driver not found." };
            }

            var driverResponse = _mapper.Map<DriverResponse>(driver);
            return new ServiceResponse<DriverResponse> { Data = driverResponse };
        }

        public async Task<ServiceResponse<List<DriverResponse>>> AddDriverAsync(DriverRequest driverRequest)
        {
            var driver = _mapper.Map<Driver>(driverRequest);
            await _driversCollection.InsertOneAsync(driver);
            var drivers = await _driversCollection.Find(_ => true).ToListAsync();
            var driverResponses = _mapper.Map<List<DriverResponse>>(drivers);
            return new ServiceResponse<List<DriverResponse>> { Data = driverResponses };
        }

        public async Task<ServiceResponse<DriverResponse>> UpdateDriverAsync(string id, DriverRequest driverRequest)
        {
            var driver = await _driversCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (driver == null)
            {
                return new ServiceResponse<DriverResponse> { Success = false, Message = "Driver not found." };
            }

            _mapper.Map(driverRequest, driver);
            await _driversCollection.ReplaceOneAsync(x => x.Id == id, driver);

            var updatedDriver = _mapper.Map<DriverResponse>(driver);
            return new ServiceResponse<DriverResponse> { Data = updatedDriver };
        }

        public async Task<ServiceResponse<List<DriverResponse>>> DeleteDriverAsync(string id)
        {
            var deleteResult = await _driversCollection.DeleteOneAsync(x => x.Id == id);
            if (deleteResult.DeletedCount == 0)
            {
                return new ServiceResponse<List<DriverResponse>> { Success = false, Message = "Driver not found." };
            }

            var drivers = await _driversCollection.Find(_ => true).ToListAsync();
            var driverResponses = _mapper.Map<List<DriverResponse>>(drivers);
            return new ServiceResponse<List<DriverResponse>> { Data = driverResponses };
        }
    }
}
