using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drivers.Api.DTO;
using Drivers.Api.Models;


namespace dotnet_test.Mapping
{
    public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Driver, DriverResponse>();
        CreateMap<DriverRequest, Driver>();
    }
}
}