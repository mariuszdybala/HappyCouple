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
        public event Action<ProductVm> ProductDeleteButtonClick;
        public event Action<ProductVm> ProductEditButtonClick;
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

        public void AddProductsToContainer(IList<ProductVm> products)
        {
            Products.AddRange(products);

            foreach (var product in Products)
            {
                CreateNewProductViewControl(product);
            }
        }

        public void AddProductToContainer(ProductVm product)
        {
            Products.Insert(0,product);

            CreateNewProductViewControl(product);
        }

        private void CreateNewProductViewControl(ProductVm product)
        {
            var productView = new ProductViewControl { Product = product };


            //AssignEvents(productView);

            //ProductsContainer.Children.Insert(0,productView);
        }

        public void UnSubscribeAllEvents()
        {
//            foreach (var productViewControl in ProductsContainer.Children.OfType<ProductViewControl>())
//            {
//                //UnSubscribeEventsFromProductViewControl(productViewControl);
//            }
        }

        public void HideControlPanels(ProductVm product)
        {
//            foreach (var productViewControl in ProductsContainer.Children.OfType<ProductViewControl>().Where(x => x.Product.Id != product.Id))
//            {
//                productViewControl.HideControlPanel();
//            }
        }

        public void DeleteProductFromView(ProductVm product)
        {
//            Products.Remove(product);
//            var productView = ProductsContainer.Children.OfType<ProductViewControl>().FirstOrDefault(x=>x.Product.Id == product.Id);
//
//            ProductsContainer.Children.Remove(productView);
        }

        private void OnControlPanelInvoked(ProductVm product)
        {
            ProductControlPanelInvoked?.Invoke(product);
        }

        private void OnDeleteButtonClick(ProductVm product)
        {
            ProductDeleteButtonClick?.Invoke(product);
        }

        private void OnEditButtonClick(ProductVm product)
        {
            ProductEditButtonClick?.Invoke(product);
        }

        private void OnProductChecked(ProductVm product)
        {
            ProductChecked?.Invoke(product);
        }

//        private void AssignEvents(ProductViewControl productViewControl)
//        {
//            productViewControl.ControlPanelInvoked += OnControlPanelInvoked;
//            productViewControl.DeleteButtonClick += OnDeleteButtonClick;
//            productViewControl.Checked += OnProductChecked;
//            productViewControl.EditButtonClick += OnEditButtonClick;
//        }
//
//        private void UnSubscribeEventsFromProductViewControl(ProductViewControl productViewControl)
//        {
//            productViewControl.ControlPanelInvoked -= OnControlPanelInvoked;
//            productViewControl.DeleteButtonClick -= OnDeleteButtonClick;
//            productViewControl.Checked -= OnProductChecked;
//            productViewControl.EditButtonClick -= OnEditButtonClick;
//        }
    }
}
