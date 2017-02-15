using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.ViewModel.Abstract;

namespace HappyCoupleMobile.ViewModel
{
    public class FavouriteProductsViewModel : BaseHappyViewModel
    {
        private Dictionary<ProductType, IList<Product>> _productsTypesWithProducts;
        private readonly IProductServices _productService;
        private int _carouselPosition;
        public ObservableCollection<ProductType> FavouritesProductTypes { get; set; }

        public int CarouselPosition
        {
            get { return _carouselPosition; }
            set { Set(ref _carouselPosition, value); }
        }

        public FavouriteProductsViewModel(ISimpleAuthService simpleAuthService, IProductServices productService) : base(simpleAuthService)
        {
            _productService = productService;
            _productsTypesWithProducts = new Dictionary<ProductType, IList<Product>>();
            FavouritesProductTypes = new ObservableCollection<ProductType>();
            RegisterCommandAndMessages();
        }

        public void RegisterCommandAndMessages()
        {
            MessengerInstance.Register<IBaseMessage<FavouriteProductsViewModel>>(this, async (message) => await OnNavigateTo(message));
        }
        private async Task OnNavigateTo(IBaseMessage<FavouriteProductsViewModel> message)
        {
            await LoadProductTypes();
        }

        private async Task LoadProductTypes()
        {
            _productsTypesWithProducts = await _productService.GetFavouriteTaskProductTypesWithProductsAsync();
            var productTypes =await _productService.GetAllProductTypesAync();

            FavouritesProductTypes = new ObservableCollection<ProductType>(productTypes);

            CarouselPosition = 4;

            RaisePropertyChanged(nameof(FavouritesProductTypes));
        }

        protected override void CleanResources()
        {
            // ProductTypes = new ObservableCollection<ProductType>();
        }
    }
}