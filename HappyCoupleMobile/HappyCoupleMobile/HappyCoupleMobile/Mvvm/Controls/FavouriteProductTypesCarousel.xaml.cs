using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class FavouriteProductTypesCarousel : CarouselViewControl
    {
        public static BindableProperty FavouritesProductTypesProperty = BindableProperty
        .Create(nameof(FavouritesProductTypes), typeof(ObservableCollection<ProductType>), typeof(FavouriteProductTypesCarousel));

        public static BindableProperty PositionProperty = BindableProperty.Create
        (nameof(Position), typeof(int), typeof(FavouriteProductTypesCarousel), 0);

        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
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

            Position = 4;
        }

        private void OnPositionSelected(object sender, EventArgs args)
        {
        }
    }
}
