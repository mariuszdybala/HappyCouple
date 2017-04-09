using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class EditProductListControl : StackLayout
    {
        public static BindableProperty ProductsProperty = BindableProperty
        .Create(nameof(Products), typeof(ObservableCollection<ProductVm>), typeof(EditProductListControl), propertyChanged: OnProductsChanged);

        public static BindableProperty ShowCheckboxesProperty = BindableProperty
        .Create(nameof(ShowCheckboxes), typeof(bool), typeof(EditProductListControl), false);

        public static BindableProperty IsCheckedProperty = BindableProperty
        .Create(nameof(IsChecked), typeof(bool), typeof(EditProductListControl), false);

        public static BindableProperty ShowControlPanelProperty = BindableProperty
         .Create(nameof(ShowControlPanel), typeof(bool), typeof(EditProductListControl), false);

        public static BindableProperty ProductSelectedCommandProperty = BindableProperty.Create
        (nameof(ProductSelectedCommand), typeof(ICommand), typeof(EditProductListControl));

        public static BindableProperty ProductCheckedCommandProperty = BindableProperty.Create
        (nameof(ProductCheckedCommand), typeof(ICommand), typeof(EditProductListControl));

        public static BindableProperty ButtonDeleteCommandProperty = BindableProperty.Create
        (nameof(ButtonDeleteCommand), typeof(ICommand), typeof(EditProductListControl));

        public static BindableProperty ButtonEditCommandProperty = BindableProperty.Create
        (nameof(ButtonEditCommand), typeof(ICommand), typeof(EditProductListControl));

        public static BindableProperty UnSubscribeAllEventsCommandProperty = BindableProperty.Create
        (nameof(UnSubscribeAllEventsCommand), typeof(ICommand), typeof(EditProductListControl), defaultBindingMode: BindingMode.OneWayToSource);

        public ICommand UnSubscribeAllEventsCommand
        {
            get { return (ICommand)GetValue(UnSubscribeAllEventsCommandProperty); }
            set { SetValue(UnSubscribeAllEventsCommandProperty, value); }
        }

        public ICommand ButtonEditCommand
        {
            get { return (ICommand)GetValue(ButtonEditCommandProperty); }
            set { SetValue(ButtonEditCommandProperty, value); }
        }

        public ICommand ButtonDeleteCommand
        {
            get { return (ICommand)GetValue(ButtonDeleteCommandProperty); }
            set { SetValue(ButtonDeleteCommandProperty, value); }
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

        public ObservableCollection<ProductVm> Products
        {
            get { return (ObservableCollection<ProductVm>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public EditProductListControl()
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
            var editProductsListControl = (EditProductListControl)bindable;

            InitialProductList(products, editProductsListControl);
        }

        private static void InitialProductList(ObservableCollection<ProductVm> products, EditProductListControl editProductsListControl)
        {
            editProductsListControl.UnSubscribeFromProductListEvents(products);

            if (!products.Any())
            {
                return;
            }
            var productsTypes = products.GroupBy(x => x.ProductType).Select(x => new { Type = x.Key, Products = x });

            foreach (var productType in productsTypes)
            {
                editProductsListControl.InsertNewProductsToNewType(productType.Type, productType.Products.ToList());
            }

            editProductsListControl.AssingEventsToProductList(products);
        }

        private void InsertProduct(ProductVm product)
        {
            var productTypePanelForNewProduct = ProductTypesPanelsContainer.Children.OfType<ProductTypePanelControl>().FirstOrDefault(x => x.ProductType.Id == product.ProductType.Id);

            if (productTypePanelForNewProduct == null)
            {
                InsertNewProductsToNewType(product.ProductType, new List<ProductVm> { product});
            }

            else
            {
                InsertNewProductsToExistingType(productTypePanelForNewProduct, product);
            }

        }

        private void InsertNewProductsToNewType(ProductType productType, IList<ProductVm> products)
        {
            var productTypePanel = CreateNewProductTypePanelControl(productType, products.ToList());

            ProductTypesPanelsContainer.Children.Insert(0,productTypePanel);
        }

        private void InsertNewProductsToExistingType(ProductTypePanelControl productTypePanelControl, ProductVm product)
        {
            productTypePanelControl.AddProductToContainer(product);
        }

        private void ProductList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var newProduct = (ProductVm) e.NewItems[0];
                InsertProduct(newProduct);
            }
        }

        private ProductTypePanelControl CreateNewProductTypePanelControl(ProductType productType, List<ProductVm> products)
        {
            var productTypePanel = new ProductTypePanelControl();

            productTypePanel.SetContainerData(productType);
            productTypePanel.AddProductsToContainer(products);

            AssignEvents(productTypePanel);

            return productTypePanel;
        }

        private void AssignEvents(ProductTypePanelControl productTypePanelControl)
        {
            productTypePanelControl.ProductControlPanelInvoked += OnControlPanelInvoked;
            productTypePanelControl.ProductDeleteButtonClick += OnDeleteButtonClickProductButtonClick;
            productTypePanelControl.ProductChecked += OnProductChecked;
            productTypePanelControl.ProductEditButtonClick += OnEditProductButtonClick;
        }

        private void OnEditProductButtonClick(ProductVm product)
        {
            if (ButtonEditCommand == null)
            {
                return;
            }

            if (ButtonEditCommand.CanExecute(product))
            {
                ButtonEditCommand.Execute(product);
            }
        }

        private void OnDeleteButtonClickProductButtonClick(ProductVm product)
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
            var productTypePanel = ProductTypesPanelsContainer.Children.OfType<ProductTypePanelControl>().FirstOrDefault(x => x.ProductType.Id == product.ProductType.Id);

            productTypePanel.DeleteProductFromView(product);

            if (!productTypePanel.Products.Any())
            {
                ProductTypesPanelsContainer.Children.Remove(productTypePanel);
            }
        }

        private void OnProductChecked(ProductVm product)
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

        private void OnControlPanelInvoked(ProductVm product)
        {

            foreach (var productTypePanelControl in ProductTypesPanelsContainer.Children.OfType<ProductTypePanelControl>())
            {
                productTypePanelControl.HideControlPanels(product);
            }
        }

        private void OnUnSubscribeAllEvents()
        {
            UnSubscribeFromProductListEvents(Products);

            foreach (var productTypePanelControl in ProductTypesPanelsContainer.Children.OfType<ProductTypePanelControl>())
            {
                productTypePanelControl.UnSubscribeAllEvents();
            }
        }

        public void AssingEventsToProductList(ObservableCollection<ProductVm> productList)
        {
            productList.CollectionChanged += ProductList_CollectionChanged;
        }

        public void UnSubscribeFromProductListEvents(ObservableCollection<ProductVm> productList)
        {
            productList.CollectionChanged -= ProductList_CollectionChanged;
        }
    }
}
