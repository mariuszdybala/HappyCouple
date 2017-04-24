using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ProductTypesContainer : StackLayout
    {
        public static readonly BindableProperty ProductTypesProperty = BindableProperty.Create(
        nameof(ProductTypes), typeof(ObservableCollection<ProductType>), typeof(ShoppingListPanel), propertyChanged: OnProductTypesChanged);

        public ObservableCollection<ProductType> ProductTypes
        {
            get { return (ObservableCollection<ProductType>)GetValue(ProductTypesProperty); }
            set { SetValue(ProductTypesProperty, value); }
        }

        public ProductTypesContainer()
        {
            InitializeComponent();
        }

        private static void OnProductTypesChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var productTypesContainer = (ProductTypesContainer)bindable;
            var productTypes = (ObservableCollection<ProductType>)newvalue;

            if (productTypes.Any())
            {
                AddProductTypesToProductTypesContainer(productTypes, productTypesContainer);
            }
        }

        private static void AddProductTypesToProductTypesContainer(IList<ProductType> productTypes, ProductTypesContainer productTypesContainer)
        {
            productTypesContainer.Children.Clear();

            foreach (var type in productTypes)
            {
                if (productTypesContainer.Children.Count == 7)
                {
                    productTypesContainer.Children.Add(
                        new Image
                        {
                            Source = (FileImageSource)Application.Current.Resources["AddToList"],
                            HeightRequest = 25
                        }
                        );
                    return;
                }

                FileImageSource imageSource = Application.Current.Resources.ContainsKey(type.IconName)
                    ? Application.Current.Resources[type.IconName] as FileImageSource
                    : Application.Current.Resources["Other"] as FileImageSource;

                if (imageSource == null)
                {
                    continue;
                }

                productTypesContainer.Children.Add(new Image { Source = imageSource, HeightRequest = 25 });
            }
        }
    }
}
