using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        public WeatherData()
        {
            Temperature = new Temperature();
            Wind = new Wind();
            Coordinates = new Coordinates();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public double Time { get; set; }
        public double Sunrise { get; set; }
        public double Sunset { get; set; }
        public Temperature Temperature { get; set; }
        public Wind Wind { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
