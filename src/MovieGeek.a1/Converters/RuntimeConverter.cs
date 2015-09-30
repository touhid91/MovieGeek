using System;
using System.Globalization;
using System.Windows.Data;

namespace MovieGeek.a1.Converters
{
    public class RuntimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return String.Empty;
            try
            {
                int temp = Int32.Parse(value.ToString().Substring(0, 3));
                return temp % 60 != 0 ? String.Format("{0}h {1}m", temp / 60, temp % 60) : String.Format("{0}h", temp / 60);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}