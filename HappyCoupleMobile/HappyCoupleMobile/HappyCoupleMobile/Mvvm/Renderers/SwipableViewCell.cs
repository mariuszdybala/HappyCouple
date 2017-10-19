using System;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers
{
	public class SwipableViewCell : ViewCell
	{
        public static BindableProperty ProductProperty =
            BindableProperty.Create(nameof(Product), typeof(ProductVm), typeof(SwipableViewCell));

        public ProductVm Product
        {
            get => (ProductVm)GetValue(ProductProperty);
            set => SetValue(ProductProperty, value);
        }

        public void OnProductChecked()
        {
            
        }
	}
}
