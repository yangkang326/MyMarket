#region

using System;
using System.Globalization;
using System.Windows.Data;

#endregion

namespace MyMarket.Converter
{
    public class ValueToString : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse(value.ToString());
        }
    }
}