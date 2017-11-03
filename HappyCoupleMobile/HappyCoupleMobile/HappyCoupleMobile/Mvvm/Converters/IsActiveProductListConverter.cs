using System;
using System.Globalization;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Converters
{
	public class IsActiveProductListConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var status = (ShoppingListStatus)value;

			return status == ShoppingListStatus.Active;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
