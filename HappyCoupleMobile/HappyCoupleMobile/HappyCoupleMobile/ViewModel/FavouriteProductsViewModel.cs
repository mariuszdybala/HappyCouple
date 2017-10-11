using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.ViewModel.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.View;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class FavouriteProductsViewModel : BaseHappyViewModel
    {
        private readonly IProductServices _productService;
        private ProductType _selectedProductType;

        public ProductType SelectedProductType
        {
            get { return _selectedProductType; }
            set { Set(ref _selectedProductType, value); }
        }

        public ObservableCollection<ProductType> ProductTypes { get; set; }
        public ObservableCollection<ProductVm> MockListForProduct { get; set; }

        public RelayCommand<ProductVm> DeleteProductCommand { get; set; }
        public RelayCommand<ProductVm> ProductSelectedCommand { get; set; }
	    public RelayCommand<ProductVm> EditProductCommand => new RelayCommand<ProductVm>(async (product) => await OnEditProduct(product));
	    public RelayCommand<ProductVm> GoToAddProductToFavoriteCommand => new RelayCommand<ProductVm>(async async  => await OnGoToAddProductToFavorite());

	    public RelayCommand UnSubscribeAllEventsFromViewCommand { get; set; }
        public RelayCommand<ProductType> ProductTypeSelectedCommand { get; set; }

        public FavouriteProductsViewModel(ISimpleAuthService simpleAuthService, IProductServices productService) : base(simpleAuthService)
        {
            _productService = productService;
            ProductTypes = new ObservableCollection<ProductType>();
            MockListForProduct = new ObservableCollection<ProductVm>();
            RegisterCommandAndMessages();
        }

        public void RegisterCommandAndMessages()
        {
            RegisterNavigateToMessage(this);

            ProductSelectedCommand = new RelayCommand<ProductVm>(async(product) => await OnProductSelected(product));
            DeleteProductCommand = new RelayCommand<ProductVm>(OnDeleteProduct);

            ProductTypeSelectedCommand = new RelayCommand<ProductType>(async(product) => await OnProductTypeSelected(product));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            LoadProductTypes();

	        await Task.Yield();

        }

	    private async Task OnGoToAddProductToFavorite()
	    {
		    await NavigateToWithMessage<AddProductView, AddProductViewModel>(new BaseMessage<AddProductViewModel>(MessagesKeys.ProductTypeKey, SelectedProductType));
	    }

        private void OnDeleteProduct(ProductVm product)
        {
        }

	    private async Task OnEditProduct(ProductVm product)
	    {
		    await Task.Yield();
	    }

        private async Task OnProductTypeSelected(ProductType productType)
        {
            await Task.Yield();
        }

        private async Task OnProductSelected(ProductVm product)
        {
            await Task.Yield();
        }

        private void LoadProductTypes()
        {
            var mockListForProduct =
                new List<Product>
                {
                    new Product
                    {
                        Id = 0,
                        Name = "Marcheweczka",
                        Comment = "To jest pyszna marcheweczka trzeba ją kupić",
                        Quantity = 4
                    },
                    new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 4, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 6, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 8, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10}
	            };

            MockListForProduct = new ObservableCollection<ProductVm>(mockListForProduct.Select(x=> new ProductVm(x)));

            RaisePropertyChanged(nameof(MockListForProduct));
        }

        protected override void CleanResources()
        {

        }
    }
}
