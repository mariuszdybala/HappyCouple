using System.Collections.Generic;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel.Interfaces
{
    public interface IShoppingListViewModel
    {
        Command<ShoppingList> DeleteListCommand { get; set; }
        Command<ShoppingList> AddProductToListCommand { get; set; }
        Command<ShoppingList> CloseListCommand { get; set; }
        Command<ShoppingList> EditListCommand { get; set; }
        IList<ShoppingList> ShoppingLists { get; set; }
    }
}