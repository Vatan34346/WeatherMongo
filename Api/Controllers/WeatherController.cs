using Api.ControllerDTOs;
using Api.ControllerExtentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {

        private readonly IWeatherServices _weatherService;

        public WeatherController(IWeatherServices weatherService)
        {
            _weatherService = weatherService;
        }


        [HttpGet]
        public async Task<IEnumerable<WeatherApi>> GetAllWeatherAsync()
        {
            var serviceModels = await _weatherService.GetAllWeatherAsync();

            List<WeatherApi> weatherApis = new List<WeatherApi>();
            foreach(WeatherDTO item in serviceModels)
            {
                weatherApis.Add(item.ToModel());
            }

            return weatherApis;
        }

        [HttpPost("AddWeather")]
        public async Task<ActionResult<CreateWeatherModel>> CreateWeatherRecordAsync(CreateWeatherModel model)
        {
            if(model is null)
            {
              return  BadRequest();
            }

            var apiModel = model.ToApiModel();

            var serviceModel = apiModel.ToSericeDTO();
            await _weatherService.CreateWeatherAsync(serviceModel);

            return Created(serviceModel.Id.ToString(),model);
        }

        [HttpPut("UpdateWeather")]
        public async Task<ActionResult> UpdateWeatherRecord(WeatherApi weatherApi)
        {
            if(weatherApi is null)
            {
                return BadRequest();
            }
            var serviceDTO = weatherApi.ToSericeDTO();
            await _weatherService.UpdateWeatherAsync(serviceDTO);

            return NoContent();
        } 

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWeatherRecord(Guid id)
        {
            var foundRecord = await _weatherService.GetWeatherByIdAsync(id);
            if(foundRecord is null)
            {
               return NotFound();
            }

            await _weatherService.DeleteWeatherAsync(id);
            return NoContent();
        }
    }
}
