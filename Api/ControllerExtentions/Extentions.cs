using Api.ControllerDTOs;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ControllerExtentions
{
    public static class Extentions
    {
        public static WeatherApi ToApiModel(this CreateWeatherModel modle)
        {
            return new WeatherApi()
            {
                Id = Guid.NewGuid(),
                TemperatureC=modle.TemperatureC,
                CreationTime=DateTimeOffset.UtcNow
            };
        }



        public static WeatherApi ToModel(this WeatherDTO weather)
        {
            return new WeatherApi()
            {
                Id = weather.Id,
                TemperatureC = weather.TemperatureC,
                CreationTime = weather.CreationTime
            };
        }

        public static WeatherDTO ToSericeDTO(this WeatherApi weather)
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
