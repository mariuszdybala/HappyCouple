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

        public async Task DeleteProductTypeWithoutFavourite(ProductType productType)
        {
            if (productType.IsFavourite && productType.IsPrimary)
            {
                return;
            }

            await DeleteAsync(productType);
        }

        public async Task DeleteProductType(ProductType product)
        {
            if (product.IsPrimary)
            {
                return;
            }

            await DeleteAsync(product);
        }

        public async Task<IList<ProductType>> GetAllProductTypesAddedByUser()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<ProductType>().Where(x => !x.IsPrimary).ToListAsync();
        }

        public async Task<IList<ProductType>> GetAllProductTypesPrimary()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<ProductType>().Where(x => x.IsPrimary).ToListAsync();
        }

        public async Task<IList<ProductType>> GetAllProductTypesFavorite()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<ProductType>().Where(x => x.IsFavourite).ToListAsync();
        }
    }
}