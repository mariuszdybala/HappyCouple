using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class ShoppingListViewModel : BaseHappyViewModel
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        public ICommand RightIconTapCommand { get; set; }
        public Command<ShoppingList> DeleteListCommand { get; set; }
        public Command<ShoppingList> AddProductToListCommand { get; set; }
        public Command<ShoppingList> CloseListCommand { get; set; }
        public Command<ShoppingList> EditListCommand { get; set; }


        public ObservableCollection<ShoppingList> ActiveShoppingLists { get; set; }
        public ObservableCollection<ShoppingList> ClosedShoppingLists { get; set; }

        public ShoppingListViewModel(ISimpleAuthService simpleAuthService, IShoppingListRepository shoppingListRepository) : base(simpleAuthService)
        {
            _shoppingListRepository = shoppingListRepository;
            RegisterCommand();

            ActiveShoppingLists = new ObservableCollection<ShoppingList>();
        }

        private void RegisterCommand()
        {
            RightIconTapCommand = new Command(async () => await OnRightIconTap());
            DeleteListCommand = new Command<ShoppingList>(OnDeleteList);
            AddProductToListCommand = new Command<ShoppingList>(OnAddProductToList);
            CloseListCommand = new Command<ShoppingList>(OnCloseList);
            EditListCommand = new Command<ShoppingList>(OnEditList);
        }

        public async Task GetAllShoppingListsAndInitView()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();

            ActiveShoppingLists = new ObservableCollection<ShoppingList>(shoppingLists);

            RaisePropertyChanged(nameof(ActiveShoppingLists));
        }

        private void OnEditList(ShoppingList shoppingList)
        {
        }

        private void OnCloseList(ShoppingList shoppingList)
        {
        }

        private void OnAddProductToList(ShoppingList shoppingList)
        {
        }

        private void OnDeleteList(ShoppingList shoppingList)
        {

        }


        private async Task OnRightIconTap()
        {
            IList<ShoppingList> lists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();
        }

    }
}