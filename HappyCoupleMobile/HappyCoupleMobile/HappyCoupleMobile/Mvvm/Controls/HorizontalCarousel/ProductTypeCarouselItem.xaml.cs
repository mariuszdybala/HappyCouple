using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.HorizontalCarousel
{
    public partial class ProductTypeCarouselItem : Frame
    {
        public event Action<ProductType> ProductTypeSelected;

        public static readonly BindableProperty ProductTypeProperty = BindableProperty.Create(
        nameof(ProductType), typeof(ProductType), typeof(ProductTypeCarouselItem));

        public static BindableProperty IsSelectedProperty = BindableProperty.Create
        (nameof(ProductType), typeof(bool), typeof(ProductTypeCarouselItem), false, propertyChanged: OnIsSelectedChanged);

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public ProductType ProductType
        {
            get { return (ProductType)GetValue(ProductTypeProperty); }
            set { SetValue(ProductTypeProperty, value); }
        }

        public ICommand ProductTypeSelectedCommand => new Command(OnProductTypeSelected);

        public ProductTypeCarouselItem()
        {
            InitializeComponent();
        }

        private void OnProductTypeSelected()
        {
            IsSelected = true;
            BackgroundColor = (Color)Application.Current.Resources["ThirthColor"];

            if (IsSelected)
            {
                ProductTypeSelected?.Invoke(ProductType);
            }
        }

        private static void OnIsSelectedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var isSelected = (bool)newvalue;
            var productTypeView = (ProductTypeCarouselItem)bindable;

            productTypeView.BackgroundColor = isSelected ? (Color)Application.Current.Resources["ThirthColor"]
                                                         : (Color)Application.Current.Resources["SecondColor"];
            productTypeView.ProductTypeNameLabel.TextColor = isSelected ? Color.Black : Color.White;
        }
    }
}
