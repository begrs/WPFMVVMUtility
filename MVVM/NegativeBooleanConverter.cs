using System;
using System.Globalization;
using System.Windows.Data;
namespace WPFMVVMUtility
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class NegativeBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
