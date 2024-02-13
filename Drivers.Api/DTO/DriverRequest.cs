using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace Drivers.Api.DTO
{
    public class DriverRequest
    {
    [BsonElement("Name")]
      public string Name { get; set; } = string.Empty; 


      
      public int Number { get; set; }

      public string Team { get; set; } = string.Empty;
        
    }
}