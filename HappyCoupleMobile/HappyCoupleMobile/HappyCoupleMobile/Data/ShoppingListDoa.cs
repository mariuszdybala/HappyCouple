using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Data
{
    public class ShoppingListDao : BaseDao<ShoppingList>, IShoppingListDao
    {
        public ShoppingListDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
        {
        }

	    public async Task<IList<ShoppingList>> GetActiveShoppingList()
	    {
		    SQLiteAsyncConnection connection =  GetConnection();

		    var result = await connection.Table<ShoppingList>().Where(x => x.Status == ShoppingListStatus.Active).ToListAsync().ConfigureAwait(false);
		    return result;
	    }
	    
	    public async Task<IList<ShoppingList>> GetClosedShoppingList()
	    {
		    SQLiteAsyncConnection connection =  GetConnection();
		    
		    var result = await connection.Table<ShoppingList>().Where(x => x.Status == ShoppingListStatus.Closed).ToListAsync().ConfigureAwait(false);
		    return result;
	    }
    }
}