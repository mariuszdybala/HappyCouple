using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Mvvm.Controls.EditProductList;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.FavouriteProductList
{
    public partial class FavouriteProductListControl : StackLayout
    {
        public static BindableProperty ProductsProperty = BindableProperty
        .Create(nameof(Products), typeof(ObservableCollection<ProductVm>), typeof(FavouriteProductListControl), propertyChanged: OnProductsChanged);

        public static BindableProperty ButtonDeleteCommandProperty = BindableProperty.Create
        (nameof(ButtonDeleteCommand), typeof(ICommand), typeof(FavouriteProductListControl));

        public static BindableProperty ButtonSelectProductCommandProperty = BindableProperty.Create
        (nameof(ButtonSelectProductCommand), typeof(ICommand), typeof(FavouriteProductListControl));

        public static BindableProperty UnSubscribeAllEventsCommandProperty = BindableProperty.Create
       (nameof(UnSubscribeAllEventsCommand), typeof(ICommand), typeof(FavouriteProductListControl), defaultBindingMode: BindingMode.OneWayToSource);

        public ICommand UnSubscribeAllEventsCommand
        {
            get { return (ICommand)GetValue(UnSubscribeAllEventsCommandProperty); }
            set { SetValue(UnSubscribeAllEventsCommandProperty, value); }
        }

        public ObservableCollection<ProductVm> Products
        {
            get { return (ObservableCollection<ProductVm>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public ICommand ButtonDeleteCommand
        {
            get { return (ICommand)GetValue(ButtonDeleteCommandProperty); }
            set { SetValue(ButtonDeleteCommandProperty, value); }
        }

        public ICommand ButtonSelectProductCommand
        {
            get { return (ICommand)GetValue(ButtonSelectProductCommandProperty); }
            set { SetValue(ButtonSelectProductCommandProperty, value); }
        }

        public FavouriteProductListControl()
        {
            InitializeComponent();

            UnSubscribeAllEventsCommand = new Command(OnUnSubscribeAllEvents);
        }

        private static void OnProductsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var products = (ObservableCollection<ProductVm>)newvalue;
            var editProductsListControl = (FavouriteProductListControl)bindable;

            InitialProductList(products, editProductsListControl);
        }

        private static void InitialProductList(ObservableCollection<ProductVm> products, FavouriteProductListControl favouriteProductListControl)
        {
            if (!products.Any())
            {
                return;
            }

            foreach (var product in products)
            {
                favouriteProductListControl.CreateProductViewControl(product);
            }
        }

        private void CreateProductViewControl(ProductVm product)
        {
            var productView = new ProductViewControl { Product = product };

            productView.HideEditControlItem();
            productView.HideCheckbox();

            AssignEvents(productView);

            ProductsContainer.Children.Add(productView);
        }


        private void OnDeleteButtonClick(ProductVm product)
        {
            DeleteProductFromView(product);

            if (ButtonDeleteCommand == null)
            {
                return;
            }

            if (ButtonDeleteCommand.CanExecute(product))
            {
                ButtonDeleteCommand.Execute(product);
            }
        }

        private void DeleteProductFromView(ProductVm product)
        {
            var productTypePanel = ProductsContainer.Children.OfType<ProductViewControl>().FirstOrDefault(x => x.Product.Id == product.Id);

            if (productTypePanel == null)
            {
                return;
            }

            ProductsContainer.Children.Remove(productTypePanel);
        }

        private void OnSelectProductButtonClick(ProductVm product)
        {
            if (ButtonSelectProductCommand == null)
            {
                return;
            }

            if (ButtonSelectProductCommand.CanExecute(product))
            {
                ButtonSelectProductCommand.Execute(product);
            }
        }

        private void OnControlPanelInvoked(ProductVm product)
        {
            foreach (var productViewControl in ProductsContainer.Children.OfType<ProductViewControl>().Where(x => x.Product.Id != product.Id))
            {
                productViewControl.HideControlPanel();
            }
        }

        private void AssignEvents(ProductViewControl productViewControl)
        {
            productViewControl.ControlPanelInvoked += OnControlPanelInvoked;
            productViewControl.SelectProductButtonClick += OnSelectProductButtonClick;
            productViewControl.DeleteButtonClick += OnDeleteButtonClick;
        }

        private void OnUnSubscribeAllEvents()
        {
            foreach (var productViewControl in ProductsContainer.Children.OfType<ProductViewControl>())
            {
                UnSubscribeEventsFromProductViewControl(productViewControl);
            }
        }

        private void UnSubscribeEventsFromProductViewControl(ProductViewControl productViewControl)
        {
            productViewControl.ControlPanelInvoked -= OnControlPanelInvoked;
            productViewControl.SelectProductButtonClick -= OnSelectProductButtonClick;
            productViewControl.DeleteButtonClick -= OnDeleteButtonClick;
        }
    }
}
