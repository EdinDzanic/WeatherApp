using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class StaticWeatherService : WeatherService
    {
        public WeatherData getWeatherData(string uri)
        {
            WeatherData weatherData = new WeatherData();

            weatherData.Name = "Sarajevo";
            weatherData.Description = "few clouds";
            weatherData.Sunrise = 1470195478;
            weatherData.Sunset = 1470247600;
            weatherData.Time = 1470213246;

            Temperature temperature = new Temperature();
            temperature.Humidity = 68;
            temperature.Pressure = 1020;
            temperature.Temp = 292.08f;
            temperature.Temp_Max = 292.15f;
            temperature.Temp_Min = 292.04f;
            weatherData.Temperature = temperature;

            Wind wind = new Wind();
            wind.Deg = 290;
            wind.Speed = 2.1f;
            weatherData.Wind = wind;

            Coordinates coordinates = new Coordinates();
            coordinates.Lat = 43.85f;
            coordinates.Lon = 18.36f;
            weatherData.Coordinates = coordinates;

            return weatherData;
        }
    }
}
