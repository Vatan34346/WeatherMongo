using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
   public class WeatherDTO
    {
        public Guid Id { get; set; }
        public float TemperatureC { get; set; }
        public DateTimeOffset CreationTime { get; set; }
    }
}
