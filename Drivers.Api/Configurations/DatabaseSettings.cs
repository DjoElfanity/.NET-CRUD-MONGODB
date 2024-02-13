using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drivers.Api.Configurations
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
    }
}