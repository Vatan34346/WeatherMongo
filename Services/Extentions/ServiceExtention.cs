using Domain;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Extentions
{
   public static class ServiceExtention
    {
        public static Weather ToDataBaseModel(this WeatherDTO weather)
        {
            return new Weather()
            {
                Id = weather.Id,
                TemperatureC=weather.TemperatureC,
                CreationTime=weather.CreationTime
            };
        } 

        public static WeatherDTO ToDTO(this Weather weather)
        {
            return new WeatherDTO()
            {
                Id = weather.Id,
                TemperatureC = weather.TemperatureC,
                CreationTime = weather.CreationTime
            };
        }
    }
}
