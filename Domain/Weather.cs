using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class Weather
    {
        public Guid Id { get; set; }
        public float TemperatureC { get; set; }
        public DateTimeOffset CreationTime { get; set; }


    }
}
