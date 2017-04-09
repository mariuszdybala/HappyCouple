using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.ViewModel.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class FavouriteProductsViewModel : BaseHappyViewModel
    {
        private Dictionary<string, IList<Product>> _productsTypesWithProducts;
        private readonly IProductServices _productService;
        private int _carouselPosition;

        public ObservableCollection<ProductType> FavouritesProductTypes { get; set; }
        public ObservableCollection<ProductVm> MockListForProduct { get; set; }

        public int CarouselPosition
        {
            get { return _carouselPosition; }
            set { Set(ref _carouselPosition, value); }
        }
        public Command<ProductVm> DeleteProductCommand { get; set; }
        public Command<ProductVm> ProductSelectedCommand { get; set; }

        public Command UnSubscribeAllEventsFromViewCommand { get; set; }
        public Command<ProductType> ProductTypeSelectedCommand { get; set; }

        public FavouriteProductsViewModel(ISimpleAuthService simpleAuthService, IProductServices productService) : base(simpleAuthService)
        {
            _productService = productService;
            _productsTypesWithProducts = new Dictionary<string, IList<Product>>();
            FavouritesProductTypes = new ObservableCollection<ProductType>();
            MockListForProduct = new ObservableCollection<ProductVm>();
            RegisterCommandAndMessages();
        }

        public void RegisterCommandAndMessages()
        {
            ProductSelectedCommand = new Command<ProductVm>(async(product) => await OnProductSelected(product));
            DeleteProductCommand = new Command<ProductVm>(OnDeleteProduct);

            ProductTypeSelectedCommand = new Command<ProductType>(async(product) => await OnProductTypeSelected(product));

            MessengerInstance.Register<IBaseMessage<FavouriteProductsViewModel>>(this, async (message) => await OnNavigateTo(message));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            await Task.Yield();
        }

        private void OnDeleteProduct(ProductVm product)
        {
        }

        private async Task OnProductTypeSelected(ProductType productType)
        {
            await Task.Yield();
        }

        private async Task OnProductSelected(ProductVm product)
        {
            await Task.Yield();
        }

        private async Task OnNavigateTo(IBaseMessage<FavouriteProductsViewModel> message)
        {
            await LoadProductTypes();
            await LoadFavouriteProducts();
        }

        private async Task LoadFavouriteProducts()
        {
            _productsTypesWithProducts = await _productService.GetFavouriteTaskProductTypesWithProductsAsync();
        }

        private async Task LoadProductTypes()
        {
            var productTypes =await _productService.GetAllProductTypesAync();

            FavouritesProductTypes = new ObservableCollection<ProductType>(productTypes);

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
                    new Product {Id = 2, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 4, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10}
                };

            MockListForProduct = new ObservableCollection<ProductVm>(mockListForProduct.Select(x=> new ProductVm(x)));

            CarouselPosition = 4;

            RaisePropertyChanged(nameof(FavouritesProductTypes));
            RaisePropertyChanged(nameof(MockListForProduct));
        }

        protected override void CleanResources()
        {
            // ProductTypes = new ObservableCollection<ProductType>();
            UnSubscribeAllEventsFromViewCommand.Execute(null);
        }
    }
}