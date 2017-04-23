using System;
using System.Globalization;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool) ? !((bool)value) : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}