using System.Windows.Input;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class ShoppingListViewModel : ViewModelBase
    {
        public ICommand RightIconTapCommand { get; set; }

        public ShoppingListViewModel()
        {
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            RightIconTapCommand = new Command(OnRightIconTap);
        }

        private void OnRightIconTap()
        {

        }
    }
}