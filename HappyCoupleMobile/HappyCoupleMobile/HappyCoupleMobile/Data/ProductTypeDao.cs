using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Data
{
    public class ProductTypeDao : BaseDao<ProductType>, IProductTypeDao
    {
        public ProductTypeDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
        {
        }

        public async Task<ProductType> GetProductTypeByTypeNameAsync(string name)
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<ProductType>().Where(x => x.Type == name).FirstOrDefaultAsync();
        }
    }
}