using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Helpers;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class AddProductForm : StackLayout
    {
        public static BindableProperty ProductTypesSourceProperty = BindableProperty
            .Create(nameof(ProductTypesSource), typeof(ObservableCollection<ProductType>), typeof(AddProductForm), propertyChanged: OnProductTypesSourceChanged);

        public static BindableProperty FavouritesProductTypesProperty = BindableProperty
        .Create(nameof(FavouritesProductTypes), typeof(ObservableCollection<ProductType>), typeof(AddProductForm));

        public static BindableProperty AddToFavoriteCommandProperty = BindableProperty.Create
            (nameof(AddToFavoriteCommandProperty).GetBindableName(), typeof(ICommand), typeof(AddProductForm));

        public static BindableProperty EraseEntryCommandProperty = BindableProperty.Create
            (nameof(EraseEntryCommandProperty).GetBindableName(), typeof(ICommand), typeof(AddProductForm));

        public static BindableProperty ProductProperty = BindableProperty.Create
            (nameof(ProductProperty).GetBindableName(), typeof(Product), typeof(AddProductForm));

        public static BindableProperty SelectedProductTypeProperty = BindableProperty.Create
            (nameof(SelectedProductTypeProperty).GetBindableName(), typeof(ProductType), typeof(AddProductForm));

        public ProductType SelectedProductType
        {
            get { return (ProductType)GetValue(SelectedProductTypeProperty); }
            set { SetValue(SelectedProductTypeProperty, value); }
        }

        public ObservableCollection<ProductType> FavouritesProductTypes
        {
            get { return (ObservableCollection<ProductType>)GetValue(FavouritesProductTypesProperty); }
            set { SetValue(FavouritesProductTypesProperty, value); }
        }

        public ObservableCollection<ProductType> ProductTypesSource
        {
            get { return (ObservableCollection<ProductType>)GetValue(ProductTypesSourceProperty); }
            set { SetValue(ProductTypesSourceProperty, value); }
        }

        public Product Product
        {
            get { return (Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        public ICommand AddToFavoriteCommand
        {
            get { return (ICommand)GetValue(AddToFavoriteCommandProperty); }
            set { SetValue(AddToFavoriteCommandProperty, value); }
        }

        public ICommand EraseEntryCommand
        {
            get { return (ICommand)GetValue(EraseEntryCommandProperty); }
            set { SetValue(EraseEntryCommandProperty, value); }
        }

        public AddProductForm()
        {
            InitializeComponent();

            EraseEntryCommand = new Command<Entry>(OnEraseEntry);
        }

        private void OnEraseEntry(Entry entry)
        {
            entry.Text = string.Empty;
        }

        private static void OnProductTypesSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var productTypes = (ObservableCollection<ProductType>)newvalue;
            var addProductForm = (AddProductForm)bindable;

            if (!productTypes.Any())
            {
                return;
            }

            addProductForm.InitializeProductTypesContainer(productTypes);
        }

        public void InitializeProductTypesContainer(ObservableCollection<ProductType> productTypes)
        {
            int containerCapacity = 6;
            int containersCount = productTypes.Count / containerCapacity;
            int lastContainerCapacity = productTypes.Count % containerCapacity;

            if (lastContainerCapacity == 0)
            {
                containersCount++;
            }

            for (int i = 0; i < containersCount; i++)
            {
                var container = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                ProductTypesContainer.Children.Add(container);
            }

            int currentContainer = 0;

            foreach (var productType in productTypes)
            {
                if (!ProductTypesContainer.Children.Any())
                {
                    return;
                }

                var stackContainer = ProductTypesContainer.Children[currentContainer] as StackLayout;

                var image = new Image {Margin = new Thickness(5,5), Source = (FileImageSource)Application.Current.Resources[productType.IconName] };

                stackContainer.Children.Add(image);

                if (stackContainer.Children.Count == containerCapacity)
                {
                    currentContainer++;
                }
            }

        }
    }
}
