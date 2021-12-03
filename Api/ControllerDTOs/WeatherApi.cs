using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ControllerDTOs
{
    public class WeatherApi
    {
        public Guid Id { get; set; }
        public float TemperatureC { get; set; }
        public DateTimeOffset CreationTime { get; set; }
    }
}
