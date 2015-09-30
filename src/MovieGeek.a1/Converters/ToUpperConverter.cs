using System;
using System.Windows.Data;

namespace MovieGeek.a1.Converters
{
    public class ToUpperConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = value as String;
            return s != null ? s.ToUpper() : value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}