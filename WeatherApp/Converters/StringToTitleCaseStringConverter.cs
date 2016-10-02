using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Data;

namespace WeatherApp.Converters
{
    public class StringToTitleCaseStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string original = (string)value;
            if (original == null)
                return string.Empty;
            return culture.TextInfo.ToTitleCase(original);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string original = (string)value;

            return original.ToLower();
        }
    }
}
