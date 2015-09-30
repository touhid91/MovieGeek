using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MovieGeek.a1.Converters
{
    public class PosterConverterSmall : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var pattern = "_CR{0},0,140,209_";
            var tempVal = (value as String);
            for (Int32 i = 0; i < 10; i++)
            {
                if (tempVal.Equals((value as String)))
                    tempVal = (value as String).Replace(String.Format(pattern, i), "");
            }
            value = tempVal;
            if ((value as String).Contains("@._V1_SY"))
                value = (value as String).Replace("_SY209", "_SY418");
            else if ((value as String).Contains("@._V1_SX"))
                value =
                    (value as String).Replace("_SX140", "_SX280");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
