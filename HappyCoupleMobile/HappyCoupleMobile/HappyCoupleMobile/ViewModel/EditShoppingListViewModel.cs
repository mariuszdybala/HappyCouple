using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class EditShoppingListViewModel : BaseHappyViewModel
    {
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand ClickCommand { get; set; }

        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService) : base(simpleAuthService)
        {
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            AddProductCommand = new RelayCommand(async () => await OnAddProduct());
            ClickCommand = new RelayCommand(OnClick);
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