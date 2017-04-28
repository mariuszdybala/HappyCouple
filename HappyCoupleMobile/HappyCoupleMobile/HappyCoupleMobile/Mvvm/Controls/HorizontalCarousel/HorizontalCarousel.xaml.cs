using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Helpers;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.HorizontalCarousel
{
    public partial class HorizontalCarousel : ScrollBarLessScrollView
    {
        public static readonly BindableProperty ProductTypesProperty = BindableProperty.Create(
        nameof(ProductTypes), typeof(ObservableCollection<ProductType>), typeof(HorizontalCarousel), propertyChanged: OnProductTypesChanged);

        public static BindableProperty SelectedProductTypeProperty = BindableProperty.Create
        (nameof(SelectedProductType), typeof(ProductType), typeof(HorizontalCarousel), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty ProductTypeSelecedCommandProperty = BindableProperty.Create
         (nameof(ProductTypeSelecedCommand), typeof(ICommand), typeof(HorizontalCarousel));

        public ProductType SelectedProductType
        {
            get { return (ProductType)GetValue(SelectedProductTypeProperty); }
            set { SetValue(SelectedProductTypeProperty, value); }
        }

        public ObservableCollection<ProductType> ProductTypes
        {
            get { return (ObservableCollection<ProductType>)GetValue(ProductTypesProperty); }
            set { SetValue(ProductTypesProperty, value); }
        }

        public ICommand ProductTypeSelecedCommand
        {
            get { return (ICommand)GetValue(ProductTypeSelecedCommandProperty); }
            set { SetValue(ProductTypeSelecedCommandProperty, value); }
        }

        public HorizontalCarousel()
        {
            InitializeComponent();
        }

        private static void OnProductTypesChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var horizontalCarousel = (HorizontalCarousel)bindable;
            var productTypes = (ObservableCollection<ProductType>)newvalue;

            if (productTypes.Any())
            {
                AddProductTypesToProductTypesContainer(productTypes, horizontalCarousel);
            }
        }

        private static void AddProductTypesToProductTypesContainer(IList<ProductType> productTypes, HorizontalCarousel horizontalCarousel)
        {
            horizontalCarousel.CarouselItemsContainer.Children.Clear();

            foreach (var type in productTypes)
            {
                var productTypeCarouselItem = horizontalCarousel.CreateProductTypeCarouselItem(type);
                horizontalCarousel.CarouselItemsContainer.Children.Add(productTypeCarouselItem);
            }
        }

        private ProductTypeCarouselItem CreateProductTypeCarouselItem(ProductType productType)
        {
            var productTypeCarouselItem = new ProductTypeCarouselItem { Margin = new Thickness(5, 5), ProductType = productType };
            productTypeCarouselItem.ProductTypeSelected += OnProductTypeSelected;

            return productTypeCarouselItem;
        }

        private void OnProductTypeSelected(ProductType productType)
        {
            if (SelectedProductType != null && SelectedProductType.Id != productType.Id)
            {
                ProductTypeSelecedCommand?.Execute(productType);
            }

            SelectedProductType = productType;

            UnSelectProductTypes(productType);
        }

        private void UnSelectProductTypes(ProductType productType)
        {
            foreach (var currentProductType in CarouselItemsContainer.Children.OfType<ProductTypeCarouselItem>().Where(x => x.ProductType.Id != productType.Id))
            {
                currentProductType.IsSelected = false;
            }
        }
    }
}
