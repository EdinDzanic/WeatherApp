using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Cache
    {
        public string Location { get; set; }
        public WeatherData WeatherData { get; set; }
        public DateTime LastRequestTime { get; set; }

        public bool IsCached(string location)
        {
            if (IsInitialized())
            {
                TimeSpan elapsedTimeSinceLastRequest = DateTime.Now - LastRequestTime;
                return elapsedTimeSinceLastRequest.Minutes <= 1 && Location == location;
            }
            else
                return false;
        }

        private bool IsInitialized()
        {
            return WeatherData != null && LastRequestTime != null;
        }

        internal void CacheData(WeatherData weatherData)
        {
            Location = weatherData.Name;
            WeatherData = weatherData;
            LastRequestTime = DateTime.Now;
        }
    }
}
