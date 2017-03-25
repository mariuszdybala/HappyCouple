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
                var productTypePanel = new ProductTypePanelControl();
                productTypePanel.ProductType = productType.Type;
                productTypePanel.Products = productType.Products.ToList();

                editProductsListControl.ProductTypesPanelsContainer.Children.Add(productTypePanel);
            }
        }

    }
}
