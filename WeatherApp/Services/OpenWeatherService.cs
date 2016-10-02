using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class OpenWeatherService : WeatherService
    {
        private string key;
        private Cache cache;

        public OpenWeatherService()
        {
            key = Properties.Settings.Default.WeatherServiceKey;
            cache = new Cache();
        }

        public WeatherData getWeatherData(string location)
        {
            if (IsCached(location))
                return cache.WeatherData;
            else
            {
                string uri = buildUri(location);
                string response = requestResponse(uri);

                WeatherData weatherData = tryToCreateFromJSON(response);
                if(weatherData != null)
                    cache.CacheData(weatherData);

                return weatherData;
            }
        }

        private bool IsCached(string location)
        {
            return cache.IsCached(location);
        }      

        private string buildUri(string location)
        {
            string uri = string.Format(@"http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", location, key);

            return uri;
        }

        private string requestResponse(string uri)
        {
            WebRequest request = WebRequest.Create(uri);
            string result = null;
            using (WebResponse response = request.GetResponse())
            {
                result = getResponse(response);
            }

            return result;
        }

        private string getResponse(WebResponse response)
        {
            var reader = new System.IO.StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            return result;
        }

        private WeatherData tryToCreateFromJSON(string response)
        {
            JObject responseObject = JsonConvert.DeserializeObject<JObject>(response);

            string statusCode = responseObject.GetValue("cod").ToObject<string>();
            if(statusCode == "200")
            {
                return createFromJSON(response);
            }
            else
            {
                return null;
            }
        }

        public WeatherData createFromJSON(string json)
        {
            WeatherData weatherData = new WeatherData();

            var obj = JsonConvert.DeserializeObject<JObject>(json);

            weatherData.Name = obj.GetValue("name").ToObject<string>();
            weatherData.Description = obj.GetValue("weather")[0]["description"].ToObject<string>();
            weatherData.Icon = obj.GetValue("weather")[0]["icon"].ToObject<string>();
            weatherData.Sunrise = obj.GetValue("sys")["sunrise"].ToObject<int>();
            weatherData.Sunset = obj.GetValue("sys")["sunset"].ToObject<int>();
            weatherData.Time = obj.GetValue("dt").ToObject<int>();
            weatherData.Temperature = obj.GetValue("main").ToObject<Temperature>();
            weatherData.Coordinates = obj.GetValue("coord").ToObject<Coordinates>();
            weatherData.Wind = obj.GetValue("wind").ToObject<Wind>();

            return weatherData;
        }
    }
}
