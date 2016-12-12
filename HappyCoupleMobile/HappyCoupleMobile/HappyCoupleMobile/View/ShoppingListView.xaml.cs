using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Controls;
using HappyCoupleMobile.View.Abstract;
using HappyCoupleMobile.ViewModel;
using Xamarin.Forms;

namespace HappyCoupleMobile.View
{
    public partial class ShoppingListView : BaseHappyContentPage
    {
        public ShoppingListView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        public void FeedShoppingListContainer()
        {
            ShoppingListViewModel viewModel = GetBoundViewModel<ShoppingListViewModel>();

            if (viewModel == null)
            { 
                return;
            }

            if (ShoppingListPanelContent.Children.Any())
            {
                ShoppingListPanelContent.Children.Clear();
            }

            foreach (ShoppingList shoppingList in viewModel.ShoppingLists)
            {
                ShoppingListPanelContent.Children.Add(new ShoppingListPanel
                {
                    ShoppingList = shoppingList,
                    AddCommand = viewModel.AddProductToListCommand,
                    CloseCommand = viewModel.CloseListCommand,
                    DeleteCommand = viewModel.DeleteListCommand,
                    EditOrListTappedCommand = viewModel.EditListCommand
                });
            }

        }
    }
}
