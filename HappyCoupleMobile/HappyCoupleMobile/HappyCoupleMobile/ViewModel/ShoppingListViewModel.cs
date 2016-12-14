using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using HappyCoupleMobile.ViewModel.Interfaces;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class ShoppingListViewModel : BaseHappyViewModel , IShoppingListViewModel
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        public ICommand RightIconTapCommand { get; set; }
        public Command<ShoppingList> DeleteListCommand { get; set; }
        public Command<ShoppingList> AddProductToListCommand { get; set; }
        public Command<ShoppingList> CloseListCommand { get; set; }
        public Command<ShoppingList> EditListCommand { get; set; }
        public IList<ShoppingList> ShoppingLists { get; set; }

        public ShoppingListViewModel(ISimpleAuthService simpleAuthService, IShoppingListRepository shoppingListRepository) : base(simpleAuthService)
        {
            _shoppingListRepository = shoppingListRepository;
            RegisterCommand();

            ShoppingLists = new List<ShoppingList>();
        }

        private void RegisterCommand()
        {
            RightIconTapCommand = new Command(async () => await OnRightIconTap());
            DeleteListCommand = new Command<ShoppingList>(async (shoppingList) => await OnDeleteList(shoppingList));
            AddProductToListCommand = new Command<ShoppingList>(async (shoppingList) => await OnAddProductToList(shoppingList));
            CloseListCommand = new Command<ShoppingList>(async (shoppingList) => await OnCloseList(shoppingList));
            EditListCommand = new Command<ShoppingList>(async (shoppingList) => await OnEditList(shoppingList));
        }

        public async Task GetAllShoppingListsAndInitView()
        {
            ShoppingLists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();
        }

        public  void InitializeViewWithShoppingLists()
        {
            ShoppingListView shoppingListView = Page as ShoppingListView;

            shoppingListView?.FeedShoppingListContainer();
        }

        private Task OnEditList(ShoppingList shoppingList)
        {
            return null;
        }

        private Task OnCloseList(ShoppingList shoppingList)
        {
            return null;
        }

        private Task OnAddProductToList(ShoppingList shoppingList)
        {
            return null;
        }

        private Task OnDeleteList(ShoppingList shoppingList)
        {
            return null;
        }


        private async Task OnRightIconTap()
        {
            IList<ShoppingList> lists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();
        }


    }
}