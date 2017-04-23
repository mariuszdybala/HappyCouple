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
        public static BindableProperty ProductNameProperty = BindableProperty.Create
        (nameof(ProductName), typeof(string), typeof(AddProductForm), defaultBindingMode:BindingMode.TwoWay);

        public static BindableProperty ProductCommentProperty = BindableProperty.Create
        (nameof(ProductComment).GetBindableName(), typeof(string), typeof(AddProductForm), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty ProductQuantityProperty = BindableProperty.Create
        (nameof(ProductQuantity), typeof(string), typeof(AddProductForm), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty ProductTypesSourceProperty = BindableProperty
            .Create(nameof(ProductTypesSource), typeof(ObservableCollection<ProductType>), typeof(AddProductForm), propertyChanged: OnProductTypesSourceChanged);

        public static BindableProperty AddToFavoriteCommandProperty = BindableProperty.Create
            (nameof(AddToFavoriteCommandProperty).GetBindableName(), typeof(ICommand), typeof(AddProductForm));

        public static BindableProperty SelectedProductTypeProperty = BindableProperty.Create
            (nameof(SelectedProductType), typeof(ProductType), typeof(AddProductForm), defaultBindingMode: BindingMode.TwoWay);

        public ProductType SelectedProductType
        {
            get { return (ProductType)GetValue(SelectedProductTypeProperty); }
            set { SetValue(SelectedProductTypeProperty, value); }
        }

        public ObservableCollection<ProductType> ProductTypesSource
        {
            get { return (ObservableCollection<ProductType>)GetValue(ProductTypesSourceProperty); }
            set { SetValue(ProductTypesSourceProperty, value); }
        }

        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }

        public string ProductQuantity
        {
            get { return (string)GetValue(ProductQuantityProperty); }
            set { SetValue(ProductQuantityProperty, value); }
        }

        public string ProductComment
        {
            get { return (string)GetValue(ProductCommentProperty); }
            set { SetValue(ProductCommentProperty, value); }
        }

        public ICommand AddToFavoriteCommand
        {
            get { return (ICommand)GetValue(AddToFavoriteCommandProperty); }
            set { SetValue(AddToFavoriteCommandProperty, value); }
        }

        public ICommand EraseEntryCommand => new Command<Entry>(OnEraseEntry);


        public AddProductForm()
        {
            InitializeComponent();
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
            //            foreach (var productType in productTypes)
            //            {
            //                var imageSource = (FileImageSource) Application.Current.Resources[productType.IconName];
            //                ProductTypesContainer.Children.Add(new Image {Source =  imageSource});
            //
            //            }

            int containerCapacity = 4;
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

                var productTypeView = CreateProductTypeView(productType);

                stackContainer.Children.Add(productTypeView);

                if (stackContainer.Children.Count == containerCapacity)
                {
                    currentContainer++;
                }
            }
        }

        private ProductTypeView CreateProductTypeView(ProductType productType)
        {
            var productTypeView = new ProductTypeView { Margin = new Thickness(5, 5), ProductType = productType };
            productTypeView.ProductTypeSelected += OnProductTypeSelected;

            return productTypeView;
        }

        private void OnProductTypeSelected(ProductType productType)
        {
            SelectedProductType = productType;

            UnSelectProductTypes(productType);
        }

        private void UnSelectProductTypes(ProductType productType)
        {
            foreach (var productTypeStack in ProductTypesContainer.Children.OfType<StackLayout>())
            {
                foreach (var currentProductType in productTypeStack.Children.OfType<ProductTypeView>().Where(x => x.ProductType.Id != productType.Id))
                {
                    currentProductType.IsSelected = false;
                }
            }
        }

        private void OnProductAddedToFavourite(bool isToggled)
        {
            AddToFavoriteCommand?.Execute(isToggled);
        }

        private void OnNewNameEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewNameEraseImage == null)
            {
                return;
            }

            NewNameEraseImage.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }

        private void OnQuantityEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (QuantitytEraseImage == null)
            {
                return;
            }

            QuantitytEraseImage.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }

        private void OnDescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DescriptionEraseImage == null)
            {
                return;
            }

            DescriptionEraseImage.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }
    }
}
