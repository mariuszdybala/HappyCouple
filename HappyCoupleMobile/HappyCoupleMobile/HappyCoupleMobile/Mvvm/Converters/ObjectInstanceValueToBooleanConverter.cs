using System;
using System.Globalization;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Converters
{
    public class ObjectInstanceValueToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}