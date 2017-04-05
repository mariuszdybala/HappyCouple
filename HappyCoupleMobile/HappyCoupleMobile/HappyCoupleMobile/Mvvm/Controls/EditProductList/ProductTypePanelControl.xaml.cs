using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class ProductTypePanelControl : StackLayout
    {
        public event Action<ProductVm> ProductChecked;
        public event Action<ProductVm> ProductDelete;
        public event Action<ProductVm> ProductEdit;
        public event Action<ProductVm> ProductControlPanelInvoked;

        public ProductType ProductType { get; set; }
        public List<ProductVm> Products { get; set; }

        public ProductTypePanelControl()
        {
            InitializeComponent();

            Products = new List<ProductVm>();
        }

        public void SetContainerData(ProductType productType)
        {
            ProductType = productType;

            var imageSource = Application.Current.Resources[ProductType.IconName] as FileImageSource;

            HeaderImage.Source = imageSource;
            HeaderLabel.Text = ProductType.Type;
        }

        public void LoadProducts(IList<ProductVm> products)
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

        public void UnSubscribeAllEvents()
        {
            foreach (var productViewControl in ProductsContainer.Children.OfType<ProductViewControl>())
            {
                UnSubscribeEventsFromProductViewControl(productViewControl);
            }
        }

        public void HideControlPanels(ProductVm product)
        {
            foreach (var productViewControl in ProductsContainer.Children.OfType<ProductViewControl>().Where(x => x.Product.Id != product.Id))
            {
                productViewControl.HideControlPanel();
            }
        }

        public void DeleteProductFromView(ProductVm product)
        {
            Products.Remove(product);
            var productView = ProductsContainer.Children.OfType<ProductViewControl>().FirstOrDefault(x=>x.Product.Id == product.Id);

            ProductsContainer.Children.Remove(productView);
        }

        private void OnControlPanelInvoked(ProductVm product)
        {
            ProductControlPanelInvoked?.Invoke(product);
        }

        private void OnDeleteProduct(ProductVm product)
        {
            ProductDelete?.Invoke(product);
        }

        private void OnEditProduct(ProductVm product)
        {
            ProductEdit?.Invoke(product);
        }

        private void OnProductChecked(ProductVm product)
        {
            ProductChecked?.Invoke(product);
        }

        private void AssignEvents(ProductViewControl productViewControl)
        {
            productViewControl.ControlPanelInvoked += OnControlPanelInvoked;
            productViewControl.Delete += OnDeleteProduct;
            productViewControl.Checked += OnProductChecked;
            productViewControl.Edit += OnEditProduct;
        }

        private void UnSubscribeEventsFromProductViewControl(ProductViewControl productViewControl)
        {
            productViewControl.ControlPanelInvoked -= OnControlPanelInvoked;
            productViewControl.Delete -= OnDeleteProduct;
            productViewControl.Checked -= OnProductChecked;
            productViewControl.Edit -= OnEditProduct;
        }
    }
}
