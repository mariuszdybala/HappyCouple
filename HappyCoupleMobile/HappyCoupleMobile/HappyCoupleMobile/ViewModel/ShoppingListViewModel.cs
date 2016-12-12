using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class ShoppingListViewModel : BaseHappyViewModel
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        public ICommand RightIconTapCommand { get; set; }

        public ShoppingListViewModel(ISimpleAuthService simpleAuthService, IShoppingListRepository shoppingListRepository) : base(simpleAuthService)
        {
            _shoppingListRepository = shoppingListRepository;
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            RightIconTapCommand = new Command(async () => await OnRightIconTap());
        }

        private async Task OnRightIconTap()
        {
            IList<ShoppingList> lists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();
        }
    }
}