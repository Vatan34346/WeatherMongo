using Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Abstractions;
using Services.DTOs;
using Services.Extentions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class WeatherSeervice : IWeatherServices
    {

        private const string dataName = "Weather";
        private const string collectionName = "TestingWeather";
        private readonly IMongoCollection<Weather> _mongoCollection;
        private readonly FilterDefinitionBuilder<Weather> filterBuilder = Builders<Weather>.Filter;

        public WeatherSeervice(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(dataName);
            _mongoCollection = mongoDatabase.GetCollection<Weather>(collectionName);
        }

        public async Task CreateWeatherAsync(WeatherDTO weather)
        {
            try
            {
                if(weather is null)
                {
                    throw new ArgumentNullException("Argumnet is empty or null");
                }
                var weatherContext = weather.ToDataBaseModel();
                await _mongoCollection.InsertOneAsync(weatherContext);
            }
            catch(Exception e)
            {
                Console.WriteLine("Method - CreateWeatherAsync faild: " + e.Message);
            }
        }

        public async Task DeleteWeatherAsync(Guid id)
        {
            try
            {
                if( id == null)
                {
                    throw new ArgumentNullException("Wrong id");
 
                }

                var weatherFilter = filterBuilder.Eq(existingItem => existingItem.Id, id);
                if(weatherFilter is null)
                {
                    throw new NullReferenceException("record not found");
                }

                await _mongoCollection.DeleteOneAsync(weatherFilter);
            }
            catch (Exception e)
            {

                Console.WriteLine("Method DeleteWeatherAsync failed: " + e.Message);
            }
        }

        public async Task<IEnumerable<WeatherDTO>> GetAllWeatherAsync()
        {
            try
            {
                var dbWeatherList = await _mongoCollection.Find(new BsonDocument()).ToListAsync();

                List<WeatherDTO> weatherDTOs = new List<WeatherDTO>() { };

                foreach(Weather item in dbWeatherList)
                {
                    weatherDTOs.Add(item.ToDTO());
                }
                return weatherDTOs;
            }
            catch (Exception e)
            {
                Console.WriteLine("Method GetAllWeatherAsync failed: " + e.Message);
                throw;
            }
        }

        public async Task<WeatherDTO> GetWeatherByIdAsync(Guid id)
        {
            try
            {
                var weatherFilter = filterBuilder.Eq(existingItem => existingItem.Id , id);
                if(weatherFilter is null)
                {
                    throw new Exception("Item does not exists");
                }
                if(id == null)
                {
                    throw new ArgumentNullException("Empty argumnet");
                }

                var dbWeather = await _mongoCollection.Find(weatherFilter).SingleOrDefaultAsync();

                var weatherDto = dbWeather.ToDTO();
                return weatherDto;

            }
            catch (Exception e)
            {

                Console.WriteLine("Method GetWeatherByIdAsync failed: " + e.Message);
                throw;
            }
        }

        public  async Task UpdateWeatherAsync(WeatherDTO weather)
        {
            try
            {
                if(weather is null)
                {
                    throw new ArgumentNullException("Argument are missing or wrong");
                }

                var existingWeather = filterBuilder.Eq(existingItem => existingItem.Id, weather.Id);
                if(existingWeather is null)
                {
                    throw new NullReferenceException("Item does not exists in collection");
                }

                var dbWeather = weather.ToDataBaseModel();
                await _mongoCollection.ReplaceOneAsync(existingWeather,dbWeather);

               
            }
            catch (Exception e)
            {
                Console.WriteLine("Method UpdateWeatherAsync failed: " + e.Message);
            }
        }
    }
}
