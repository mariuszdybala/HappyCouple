using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class AddProductViewModel : BaseHappyViewModel
    {
        private readonly IProductServices _productService;
        public ObservableCollection<ProductType> ProductTypes { get; set; }

        public ObservableCollection<ProductType> FavouritesProductTypes { get; set; }

        public ICommand GoToFavouriteProductsCommand { get; set; }

        public AddProductViewModel(ISimpleAuthService simpleAuthService, IProductServices productService) : base(simpleAuthService)
        {
            _productService = productService;

            ProductTypes = new ObservableCollection<ProductType>();
            RegisterCommandAndMessages();
        }

        private void RegisterCommandAndMessages()
        {
            GoToFavouriteProductsCommand = new Command(async() => await OnGoToFavouriteProducts());

            MessengerInstance.Register<IBaseMessage<AddProductViewModel>>(this, async(message) => await OnNavigateTo(message));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            await Task.Yield();
        }

        private async Task OnGoToFavouriteProducts()
        {
            await NavigateTo<FavouriteProductsView, FavouriteProductsViewModel>();
        }

        private async Task OnNavigateTo(IBaseMessage<AddProductViewModel> message)
        {
            await LoadProductTypes();
        }

        private async Task LoadProductTypes()
        {
            if (ProductTypes.Any())
            {
                return;
            }

            var productTypes = await _productService.GetAllProductTypesAync();

            ProductTypes = new ObservableCollection<ProductType>(productTypes);

            FavouritesProductTypes = new ObservableCollection<ProductType>(productTypes);

            RaisePropertyChanged(nameof(ProductTypes));
            RaisePropertyChanged(nameof(FavouritesProductTypes));
        }

        protected override void CleanResources()
        {
           // ProductTypes = new ObservableCollection<ProductType>();
        }
    }
}