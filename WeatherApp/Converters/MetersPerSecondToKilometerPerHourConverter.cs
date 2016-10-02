
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.Converters
{
    public class MetersPerSecondToKilometerPerHourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float windSpeed = (float)value;
            float windSpeedInKilometerPerHour = (windSpeed / 1000f) * 3600f;

            return string.Format("{0:0.##}", windSpeedInKilometerPerHour);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
