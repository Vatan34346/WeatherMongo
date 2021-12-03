using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
   public interface IWeatherServices
    {
        Task<IEnumerable<WeatherDTO>> GetAllWeatherAsync();

        Task<WeatherDTO> GetWeatherByIdAsync(Guid id);

        Task DeleteWeatherAsync(Guid id);

        Task UpdateWeatherAsync(WeatherDTO weather);
        Task CreateWeatherAsync(WeatherDTO weather);
    }
}
