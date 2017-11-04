using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;

namespace HappyCoupleMobile.Data
{
    public class ProductDao : BaseDao<Product>, IProductDao
    {
        public ProductDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
        {
        }

        public async Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId)
        {
            SQLiteAsyncConnection connection = GetConnection();
	        var result = await connection.GetAllWithChildrenAsync<Product>(x=>x.ShoppingListId == shoppingListId, true).ConfigureAwait(false);

	        return result;
        }

        public async Task<IList<Product>> GetAllFavouriteProductsWithChildrenForTypeAsync(int productTypeId)
        {
            SQLiteAsyncConnection connection = GetConnection();
	        
	        var result = await connection.GetAllWithChildrenAsync<Product>(x=>x.IsFavourite == true && x.ProductTypeId == productTypeId, true).ConfigureAwait(false);

	        return result;
        }
    }
}