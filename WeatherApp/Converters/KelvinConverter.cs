using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.Converters
{
    public class KelvinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float kelvin = (float)value;
            float result = kelvin -273.15f;

            return (int)result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float original = (float)value;
            float result = original + 273.15f;

            return result;
        }
    }
}
