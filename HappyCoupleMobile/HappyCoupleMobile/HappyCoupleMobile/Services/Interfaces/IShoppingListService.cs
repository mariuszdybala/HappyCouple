using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingList> AddShoppingList(IList<ShoppingList> shoppingLists, string newShoppingListName);
    }
}