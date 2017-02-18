using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ProductListView : StackLayout
    {
        public static BindableProperty ProductsProperty = BindableProperty
        .Create(nameof(Products), typeof(ObservableCollection<Product>), typeof(ProductListView));

        public static BindableProperty ProductSelectedCommandProperty = BindableProperty.Create
        (nameof(ProductSelectedCommand), typeof(ICommand), typeof(ProductListView), null);

        public ICommand ProductSelectedCommand
        {
            get { return (ICommand)GetValue(ProductSelectedCommandProperty); }
            set { SetValue(ProductSelectedCommandProperty, value); }
        }

        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public ProductListView()
        {
            InitializeComponent();
            ProductsList.RowHeight = -1;
        }

        private void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product product = e.SelectedItem as Product;

            if (ProductSelectedCommand.CanExecute(product))
            {
                ProductSelectedCommand.Execute(product);
            }
        }
    }
}
