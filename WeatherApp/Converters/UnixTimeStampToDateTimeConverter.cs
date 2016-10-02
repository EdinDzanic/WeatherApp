using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.Converters
{
    public class UnixTimeStampToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            double unixTimeStamp = (double)value;
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            string option = (string)parameter;
            if(option == "Time")
            {
                return dateTime.ToShortTimeString();
            }
            else
            {
                return dateTime.ToShortDateString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
