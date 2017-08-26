using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFMVVMUtility
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;

            if (ConverterUtility.IsNegate((string)parameter))
                val = !val;

            if (val) return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility val = (Visibility)value;

            bool result = val == Visibility.Visible;

            if(ConverterUtility.IsNegate((string) parameter))
                result = !result;
            return result;
        }
    }
}
