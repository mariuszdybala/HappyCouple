using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Converters
{
   public class ProductCountColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ShoppingListVm shoppingList = value as ShoppingListVm;
            if (shoppingList == null)
            {
                return (Color)Application.Current.Resources["FirstColor"];
            }
            else
            {
                return SetBackgroundColor(shoppingList.ProductsCount);
            }
        }

        private Color SetBackgroundColor(int shoppingListProductsCount)
        {
            if (shoppingListProductsCount >= 0 && shoppingListProductsCount < 5)
            {
                return Color.FromHex("#DAF7A6");
            }
            if (shoppingListProductsCount >= 5 && shoppingListProductsCount < 10)
            {
                return Color.FromHex("#FFC300");
            }
            if (shoppingListProductsCount >= 10 && shoppingListProductsCount < 15)
            {
                return Color.FromHex("#FF5733");
            }
            if (shoppingListProductsCount >= 15 && shoppingListProductsCount < 20)
            {
                return Color.FromHex("#C70039");
            }
            if (shoppingListProductsCount >= 20 && shoppingListProductsCount < 25)
            {
                return Color.FromHex("#900C3F");
            }

            return Color.FromHex("#581845");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
