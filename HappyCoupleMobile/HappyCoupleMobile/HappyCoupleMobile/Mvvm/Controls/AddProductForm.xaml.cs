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
    }
}
