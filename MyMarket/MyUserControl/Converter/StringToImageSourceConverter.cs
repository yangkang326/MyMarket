#region

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

#endregion

namespace MyMarket.MyUserControl.Converter
{
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string) value;
            if (!string.IsNullOrEmpty(path)) return new BitmapImage(new Uri(path, UriKind.Absolute));

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}