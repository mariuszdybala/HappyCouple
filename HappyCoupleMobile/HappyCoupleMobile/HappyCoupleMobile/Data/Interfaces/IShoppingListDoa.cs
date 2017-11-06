using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data.Interfaces
{
    public interface IShoppingListDao : IBaseDao<ShoppingList>
    {
	    Task<IList<ShoppingList>> GetActiveShoppingListAsync();
	    Task<IList<ShoppingList>> GetClosedShoppingListAsync();
    }
}