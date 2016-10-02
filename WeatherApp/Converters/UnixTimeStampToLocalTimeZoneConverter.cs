using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WeatherApp.Models;

namespace WeatherApp.Converters
{
    [ValueConversion (typeof (double), typeof (string))]
    public class UnixTimeStampToLocalTimeZoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Coordinates coordinates = parameter as Coordinates;
            double timeStamp = (double)value;

            var client = new RestClient("https://maps.googleapis.com");
            var request = new RestRequest("maps/api/timezone/json", Method.GET);
            request.AddParameter("location", (int)coordinates.Lat + "," + (int)coordinates.Lon);
            request.AddParameter("timestamp", timeStamp);
            request.AddParameter("sensor", "false");
            request.AddParameter("key", "AIzaSyCXsPxJjH6T_SA0dAX2sP4euyupmi0z_Go");
            var response = client.Execute<Models.TimeZone>(request);

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timeStamp + response.Data.rawOffset + response.Data.dstOffset);

            return dateTime.ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
