using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;

namespace HappyCoupleMobile.Data
{
    public class ShoppingListDao : BaseDao<ShoppingList>, IShoppingListDao
    {
        public ShoppingListDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
        {
        }

	    public async Task<IList<ShoppingList>> GetActiveShoppingListAsync()
	    {
		    SQLiteAsyncConnection connection =  GetConnection();

		    var result = await connection.GetAllWithChildrenAsync<ShoppingList>(x => x.Status == ShoppingListStatus.Active, true).ConfigureAwait(false);
		    return result;
	    }
	    
	    public async Task<IList<ShoppingList>> GetClosedShoppingListAsync()
	    {
		    SQLiteAsyncConnection connection =  GetConnection();
		    
		    var result = await connection.GetAllWithChildrenAsync<ShoppingList>(x => x.Status == ShoppingListStatus.Closed, true).ConfigureAwait(false);
		    return result;
	    }
    }
}