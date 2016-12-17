﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;

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

            return await connection.QueryAsync<Product>("SELECT * FROM Product WHERE shopping_list_fk=@shoppingListId",
                shoppingListId);
        }
    }
}