using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
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
    public class EditShoppingListViewModel : BaseHappyViewModel
    {
        private readonly IProductServices _propProductServices;
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand ClickCommand { get; set; }

        public ObservableCollection<ProductType> FavouritesProductTypes { get; set; }

        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService, IProductServices propProductServices) : base(simpleAuthService)
        {
            _propProductServices = propProductServices;
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            MessengerInstance.Register<IBaseMessage<EditShoppingListView>>(this, async (message) => await OnNavigateTo(message));

            AddProductCommand = new RelayCommand(async () => await OnAddProduct());
            ClickCommand = new RelayCommand(OnClick);
        }

        private async Task OnNavigateTo(IBaseMessage<EditShoppingListView> message)
        {
            var productTypes = await _propProductServices.GetAllProductTypesAync();

            FavouritesProductTypes = new ObservableCollection<ProductType>(productTypes);

            RaisePropertyChanged(nameof(FavouritesProductTypes));
        }

        private void OnClick()
        {
            
        }

        private async Task OnAddProduct()
        {
            await NavigateTo<AddProductView>();
           // IBaseMessage<AddProductViewModel>
           MessengerInstance.Send<IBaseMessage<AddProductViewModel>>(new BaseMessage<AddProductViewModel>());
        }
    }
}