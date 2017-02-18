using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ProductTypeView : Frame
    {
        public static BindableProperty ProductTypeProperty = BindableProperty.Create
        (nameof(ProductType), typeof(ProductType), typeof(ProductTypeView), null);

        public static BindableProperty IsSelectedProperty = BindableProperty.Create
        (nameof(ProductType), typeof(bool), typeof(ProductTypeView), false, propertyChanged: OnIsSelectedChanged);

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

        public ProductTypeView()
        {
            InitializeComponent();
        }

        private static void OnIsSelectedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var isSelected = (bool) newvalue;
            var productTypeView = (ProductTypeView)bindable;

            productTypeView.BackgroundColor = isSelected ? (Color)Application.Current.Resources["ThirthColor"]
                                                         : (Color)Application.Current.Resources["SecondColor"];
        }
    }
}
