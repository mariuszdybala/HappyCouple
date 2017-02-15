using System;
using System.Collections.ObjectModel;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class FavouritesProductTypesCarousel : StackLayout
    {
        public static BindableProperty FavouritesProductTypesProperty = BindableProperty
        .Create(nameof(FavouritesProductTypes), typeof(ObservableCollection<ProductType>), typeof(FavouritesProductTypesCarousel), propertyChanged: OnPropertyChnged);

        private static void OnPropertyChnged(BindableObject bindable, object oldvalue, object newvalue)
        {
            
        }

        public static BindableProperty PositionProperty = BindableProperty.Create
            (nameof(Position), typeof(int), typeof(FavouritesProductTypesCarousel), 0, propertyChanged: OnPositionChanged);

        private static void OnPositionChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            throw new NotImplementedException();
        }

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

        public FavouritesProductTypesCarousel()
        {
            InitializeComponent();

            //ProductTypesCarouselName.PositionSelected += PositionSelected;
        }

        private void PositionSelected(object sender, EventArgs eventArgs)
        {
        }
    }
}
