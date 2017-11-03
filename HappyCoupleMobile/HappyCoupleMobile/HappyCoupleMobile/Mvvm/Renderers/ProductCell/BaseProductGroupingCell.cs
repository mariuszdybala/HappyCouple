using System;
using Xamarin.Forms;
using HappyCoupleMobile.Model;
namespace HappyCoupleMobile.Mvvm.Renderers.ProductCell
{
    public class BaseProductGroupingCell : ViewCell
    {
        public bool ShowChevron { get; set;}
        public static BindableProperty ProductTypeProperty =
            BindableProperty.Create(nameof(ProductType), typeof(ProductType), typeof(BaseProductGroupingCell));
        
        public ProductType ProductType
        {
            get => (ProductType)GetValue(ProductTypeProperty);
            set => SetValue(ProductTypeProperty, value);
        }
    }
}
