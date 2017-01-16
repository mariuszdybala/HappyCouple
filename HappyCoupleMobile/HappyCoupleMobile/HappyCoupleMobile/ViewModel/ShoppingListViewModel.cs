﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private bool _showAddNewListPopUp;

        public bool ShowAddNewListPopUp
        {
            get { return _showAddNewListPopUp; }
            set { Set(ref _showAddNewListPopUp, value); }
        }

        public Command<string> AddNewListCommand { get; set; }
        public Command<ShoppingList> DeleteListCommand { get; set; }
        public Command<ShoppingList> AddProductToListCommand { get; set; }
        public Command<ShoppingList> CloseListCommand { get; set; }
        public Command<ShoppingList> EditListCommand { get; set; }
        public Command CloseAddNewListPopUpCommand { get; set; }


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
            AddNewListCommand = new Command<string>(OnAddNewListCommand);
            DeleteListCommand = new Command<ShoppingList>(OnDeleteList);
            AddProductToListCommand = new Command<ShoppingList>(OnAddProductToList);
            CloseListCommand = new Command<ShoppingList>(OnCloseList);
            EditListCommand = new Command<ShoppingList>(OnEditList);
            CloseAddNewListPopUpCommand = new Command(OnCloseAddNewListPopUpCommand);
        }



        public async Task GetAllShoppingListsAndInitView()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();

            ActiveShoppingLists = new ObservableCollection<ShoppingList>(shoppingLists);

            ClosedShoppingLists = new ObservableCollection<ShoppingList>();
            ClosedShoppingLists.Add(ActiveShoppingLists.Last());

            RaisePropertyChanged(nameof(ActiveShoppingLists));
            RaisePropertyChanged(nameof(ClosedShoppingLists));
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
        private void OnCloseAddNewListPopUpCommand()
        {
            ShowAddNewListPopUp = false;
        }

        private void OnAddNewListCommand(string newListName)
        {
            ShowAddNewListPopUp = true;
        }

    }
}