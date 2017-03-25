using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class ProductTypePanelControl : StackLayout
    {
        private ProductType _productType;
        public ProductType ProductType
        {
            get { return _productType; }
            set
            {
                _productType = value;
                SetContainerData();
            }
        }

        private IList<Product> products;

        public IList<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                LoadProducts();
            }
        }

        public ProductTypePanelControl()
        {
            InitializeComponent();
        }

        public void SetContainerData()
        {
            var imageSource = Application.Current.Resources[ProductType.IconName] as FileImageSource;

            HeaderImage.Source = imageSource;
            HeaderLabel.Text = ProductType.Type;
        }

        private void LoadProducts()
        {
            foreach (var product in Products)
            {
                var productView = new ProductViewControl();
                productView.Product = product;

                ProductsContainer.Children.Add(productView);
            }
        }
    }
}
