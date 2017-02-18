using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CarouselView.FormsPlugin.Abstractions;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class FavouriteProductTypesCarousel : CarouselViewControl
    {
        public static BindableProperty FavouritesProductTypesProperty = BindableProperty
        .Create(nameof(FavouritesProductTypes), typeof(ObservableCollection<ProductType>), typeof(FavouriteProductTypesCarousel));

        public static BindableProperty ProductPositionProperty = BindableProperty.Create
        (nameof(Position), typeof(int), typeof(FavouriteProductTypesCarousel), 0);

        public static BindableProperty ProductTypeSelectedCommandProperty = BindableProperty.Create
        (nameof(ProductTypeSelectedCommand), typeof(ICommand), typeof(FavouriteProductTypesCarousel), null);

        public ICommand ProductTypeSelectedCommand
        {
            get { return (ICommand)GetValue(ProductTypeSelectedCommandProperty); }
            set { SetValue(ProductTypeSelectedCommandProperty, value); }
        }

        public int ProductPosition
        {
            get { return (int)GetValue(ProductPositionProperty); }
            set { SetValue(ProductPositionProperty, value); }
        }

        public ObservableCollection<ProductType> FavouritesProductTypes
        {
            get { return (ObservableCollection<ProductType>)GetValue(FavouritesProductTypesProperty); }
            set { SetValue(FavouritesProductTypesProperty, value); }
        }


        public FavouriteProductTypesCarousel()
        {
            InitializeComponent();

            PositionSelected += OnPositionSelected;
        }


        private void OnPositionSelected(object sender, EventArgs args)
        {
            if (FavouritesProductTypes == null || !FavouritesProductTypes.Any())
            {
                return;
            }

            var carouselView = sender as CarouselViewControl;

            if (carouselView == null)
            {
                return;
            }

            var selectedProductType = FavouritesProductTypes[carouselView.Position];

            if (ProductTypeSelectedCommand.CanExecute(selectedProductType))
            {
                ProductTypeSelectedCommand?.Execute(selectedProductType);
            }
        }
    }
}
