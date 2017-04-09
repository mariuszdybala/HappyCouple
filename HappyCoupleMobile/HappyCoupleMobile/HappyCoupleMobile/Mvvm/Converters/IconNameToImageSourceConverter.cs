using System;
using System.Globalization;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Converters
{
    public class IconNameToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value.ToString();

            return (FileImageSource) Application.Current.Resources[iconName];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}