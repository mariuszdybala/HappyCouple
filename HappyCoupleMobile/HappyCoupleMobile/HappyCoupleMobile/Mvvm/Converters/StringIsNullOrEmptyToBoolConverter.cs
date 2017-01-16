using System;
using System.Globalization;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Converters
{
    public class StringIsNullOrEmptyToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueStr = value as string;
            var retValue = !string.IsNullOrWhiteSpace(valueStr);
//            if (parameter != null)
//            {
//                var invertBool = System.Convert.ToBoolean(parameter);
//                if (invertBool)
//                {
//                    return !retValue;
//                }
//            }
            return retValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}