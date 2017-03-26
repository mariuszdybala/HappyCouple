using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class EditProductListControl : StackLayout
    {
        public static BindableProperty ProductsProperty = BindableProperty
        .Create(nameof(Products), typeof(ObservableCollection<Product>), typeof(ProductListView), propertyChanged: OnProductsChanged);

        public static BindableProperty ShowCheckboxesProperty = BindableProperty
        .Create(nameof(ShowCheckboxes), typeof(bool), typeof(ProductListView), false);

        public static BindableProperty IsCheckedProperty = BindableProperty
        .Create(nameof(IsChecked), typeof(bool), typeof(ProductListView), false);

        public static BindableProperty ShowControlPanelProperty = BindableProperty
         .Create(nameof(ShowControlPanel), typeof(bool), typeof(ProductListView), false);

        public static BindableProperty ProductSelectedCommandProperty = BindableProperty.Create
        (nameof(ProductSelectedCommand), typeof(ICommand), typeof(ProductListView));

        public static BindableProperty ProductCheckedCommandProperty = BindableProperty.Create
        (nameof(ProductCheckedCommand), typeof(ICommand), typeof(ProductListView));

        public static BindableProperty ProductDeletedCommandProperty = BindableProperty.Create
        (nameof(ProductDeletedCommand), typeof(ICommand), typeof(ProductListView));

        public static BindableProperty ProductEditCommandProperty = BindableProperty.Create
        (nameof(ProductEditCommand), typeof(ICommand), typeof(ProductListView));

        public static BindableProperty AddProductCommandProperty = BindableProperty.Create
        (nameof(AddProductCommand), typeof(ICommand), typeof(ProductListView), defaultBindingMode:BindingMode.OneWayToSource);

        public ICommand AddProductCommand
        {
            get { return (ICommand)GetValue(AddProductCommandProperty); }
            set { SetValue(AddProductCommandProperty, value); }
        }

        public ICommand ProductEditCommand
        {
            get { return (ICommand)GetValue(ProductEditCommandProperty); }
            set { SetValue(ProductEditCommandProperty, value); }
        }

        public ICommand ProductDeletedCommand
        {
            get { return (ICommand)GetValue(ProductDeletedCommandProperty); }
            set { SetValue(ProductDeletedCommandProperty, value); }
        }

        public bool ShowControlPanel
        {
            get { return (bool)GetValue(ShowControlPanelProperty); }
            set { SetValue(ShowControlPanelProperty, value); }
        }

        public bool ShowCheckboxes
        {
            get { return (bool)GetValue(ShowCheckboxesProperty); }
            set { SetValue(ShowCheckboxesProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public ICommand ProductSelectedCommand
        {
            get { return (ICommand)GetValue(ProductSelectedCommandProperty); }
            set { SetValue(ProductSelectedCommandProperty, value); }
        }

        public ICommand ProductCheckedCommand
        {
            get { return (ICommand)GetValue(ProductCheckedCommandProperty); }
            set { SetValue(ProductCheckedCommandProperty, value); }
        }

        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public EditProductListControl()
        {
            InitializeComponent();

            AddProductCommand = new Command<Product>(OnAddProduct);
        }


        private static void OnProductsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var products = (ObservableCollection<Product>)newvalue;
            var editProductsListControl = (EditProductListControl)bindable;

            InsertProducts(products, editProductsListControl);
        }

        private static void InsertProducts(ObservableCollection<Product> products, EditProductListControl editProductsListControl)
        {
            if (!products.Any())
            {
                return;
            }
            var productsTypes = products.GroupBy(x => x.ProductType).Select(x => new { Type = x.Key, Products = x });

            foreach (var productType in productsTypes)
            {
                var productTypePanel = editProductsListControl.CreateNewProductTypePanelControl(productType.Type, productType.Products.ToList());

                editProductsListControl.ProductTypesPanelsContainer.Children.Add(productTypePanel);
            }
        }

        private ProductTypePanelControl CreateNewProductTypePanelControl(ProductType productType, List<Product> products)
        {
            var productTypePanel = new ProductTypePanelControl();

            productTypePanel.SetContainerData(productType);
            productTypePanel.LoadProducts(products);

            AssignEvents(productTypePanel);

            return productTypePanel;
        }


        private void AssignEvents(ProductTypePanelControl productTypePanelControl)
        {
            productTypePanelControl.ProductControlPanelInvoked += OnControlPanelInvoked;
            productTypePanelControl.ProductDelete += OnDeleteProduct;
            productTypePanelControl.ProductChecked += OnProductChecked;
            productTypePanelControl.ProductEdit += OnEditProduct;
        }

        private void OnAddProduct(Product product)
        {

        }

        private void OnUpdateProduct(Product product)
        {

        }

        private void OnDeleteProduct(Product product)
        {
            DeleteProductFromView(product);

            if (ProductDeletedCommand == null)
            {
                return;
            }

            if (ProductDeletedCommand.CanExecute(product))
            {
                ProductDeletedCommand.Execute(product);
            }
        }

        private void DeleteProductFromView(Product product)
        {
            var productTypePanel = ProductTypesPanelsContainer.Children.OfType<ProductTypePanelControl>().FirstOrDefault(x => x.ProductType.Id == product.ProductType.Id);

            productTypePanel.DeleteProductFromView(product);

            if (!productTypePanel.Products.Any())
            {
                ProductTypesPanelsContainer.Children.Remove(productTypePanel);
            }
        }

        private void OnEditProduct(Product product)
        {
            if (ProductEditCommand == null)
            {
                return;
            }

            if (ProductEditCommand.CanExecute(product))
            {
                ProductEditCommand.Execute(product);
            }
        }

        private void OnProductChecked(Product product)
        {
            if (ProductCheckedCommand == null)
            {
                return;
            }

            if (ProductCheckedCommand.CanExecute(product))
            {
                ProductCheckedCommand.Execute(product);
            }
        }

        private void OnControlPanelInvoked(Product product)
        {

            foreach (var productTypePanelControl in ProductTypesPanelsContainer.Children.OfType<ProductTypePanelControl>())
            {
                productTypePanelControl.HideControlPanels(product);
            }
        }
    }
}
