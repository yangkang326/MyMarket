using System;
using System.Globalization;
using System.Windows.Data;

namespace MyMarket.Conv
{
    public class PicPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return "http://39.104.103.234:8082/YK.jpg";
            }

            return "http://39.104.103.234:8082/" + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}