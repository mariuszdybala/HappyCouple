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
        public event Action<Product> ProductChecked;
        public event Action<Product> ProductDelete;
        public event Action<Product> ProductEdit;
        public event Action<Product> ProductAdd;
        public event Action<Product> ProductControlPanelInvoked;

        public ProductType ProductType { get; set; }
        public List<Product> Products { get; set; }

        public ProductTypePanelControl()
        {
            InitializeComponent();

            Products = new List<Product>();
        }

        public void SetContainerData(ProductType productType)
        {
            ProductType = productType;

            var imageSource = Application.Current.Resources[ProductType.IconName] as FileImageSource;

            HeaderImage.Source = imageSource;
            HeaderLabel.Text = ProductType.Type;
        }

        public void LoadProducts(IList<Product> products)
        {
            Products.AddRange(products);

            foreach (var product in Products)
            {
                var productView = new ProductViewControl { Product = product };

                productView.HideAddControlItem();

                AssignEvents(productView);

                ProductsContainer.Children.Add(productView);
            }
        }

        public void HideControlPanels(Product product)
        {
            foreach (var productViewControl in ProductsContainer.Children.OfType<ProductViewControl>().Where(x => x.Product.Id != product.Id))
            {
                productViewControl.HideControlPanel();
            }
        }

        public void DeleteProductFromView(Product product)
        {
            Products.Remove(product);
            var productView = ProductsContainer.Children.OfType<ProductViewControl>().FirstOrDefault(x=>x.Product.Id == product.Id);

            ProductsContainer.Children.Remove(productView);
        }

        private void AssignEvents(ProductViewControl productViewControl)
        {
            productViewControl.ControlPanelInvoked += OnControlPanelInvoked;
            productViewControl.Delete += OnDeleteProduct;
            productViewControl.Checked += OnProductChecked;
            productViewControl.Edit += OnEditProduct;
        }

        private void OnControlPanelInvoked(Product product)
        {
            ProductControlPanelInvoked?.Invoke(product);
        }

        private void OnDeleteProduct(Product product)
        {
            ProductDelete?.Invoke(product);
        }

        private void OnEditProduct(Product product)
        {
            ProductEdit?.Invoke(product);
        }

        private void OnProductChecked(Product product)
        {
            ProductChecked?.Invoke(product);
        }
    }
}
