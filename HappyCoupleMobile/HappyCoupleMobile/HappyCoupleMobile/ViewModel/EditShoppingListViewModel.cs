using System.Threading.Tasks;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class EditShoppingListViewModel : BaseHappyViewModel
    {
        public Command GoBackCommand { get; set; }

        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService) : base(simpleAuthService)
        {
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            GoBackCommand = new Command(async() => await OnGoBackCommand());
        }

        private async Task OnGoBackCommand()
        {
            await NavigateBack();
        }
    }
}