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

        public async Task DeleteProductTypeWithoutFavouriteAsync(ProductType productType)
        {
            if (productType.IsFavourite || productType.IsPrimary)
            {
                return;
            }

            await DeleteAsync(productType);
        }

        public async Task DeleteProductTypeWithoutPrimaryAsync(ProductType product)
        {
            if (product.IsPrimary)
            {
                return;
            }

            await DeleteAsync(product);
        }

        public async Task DeleteProductTypeAsync(ProductType productType)
        {
            await DeleteAsync(productType);
        }

        public async Task<IList<ProductType>> GetAllProductTypesAddedByUserAsync()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<ProductType>().Where(x => !x.IsPrimary).ToListAsync();
        }

        public async Task<IList<ProductType>> GetAllProductTypesPrimaryAsync()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<ProductType>().Where(x => x.IsPrimary).ToListAsync();
        }

        public async Task<IList<ProductType>> GetAllProductTypesFavoriteAsync()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<ProductType>().Where(x => x.IsFavourite).ToListAsync();
        }
    }
}