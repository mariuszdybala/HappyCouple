using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;

namespace HappyCoupleMobile.Repositories
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly IShoppingListDao _shoppingListDao;
        private readonly IProductDao _productDao;
        private readonly IProductTypeDao _productTypeDao;

        public ShoppingListRepository(IShoppingListDao shoppingListDao, IProductDao productDao, IProductTypeDao productTypeDao)
        {
            _shoppingListDao = shoppingListDao;
            _productDao = productDao;
            _productTypeDao = productTypeDao;
        }

        public async Task<IList<ShoppingList>> GetAllShoppingListAsync()
        {
            return await _shoppingListDao.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<IList<ShoppingList>> GetAllShoppingListWithProductsAsync()
        {
            return await _shoppingListDao.GetWithChildrenAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await _productDao.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> GetAllProductsWithChildrenAsync()
        {
            return await _productDao.GetWithChildrenAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId)
        {
            return await _productDao.GetAllProductsForShoppingListAsync(shoppingListId).ConfigureAwait(false);
        }

        public async Task<IList<ProductType>> GetAllProductTypesPrimary()
        {
            return await _productTypeDao.GetAllProductTypesPrimaryAsync();
        }

        public async Task<IList<ProductType>> GetAllProductTypesFavorite()
        {
            return await _productTypeDao.GetAllProductTypesFavoriteAsync();
        }

        public async Task<IList<ProductType>> GetAllProductTypes()
        {
            return await _productTypeDao.GetAllAsync();
        }

        public async Task InsertShoppingListAsync(ShoppingList shoppingList)
        {
            await _shoppingListDao.InsertAsync(shoppingList).ConfigureAwait(false);
        }

        public async Task InsertProductAsync(Product product)
        {
            await _productDao.InsertAsync(product).ConfigureAwait(false);
        }

        public async Task InsertProductWithChildrenAsync(Product product)
        {
            await _productDao.InsertWithChildrenAsync(product).ConfigureAwait(false);
        }

        public async Task InsertProductTypeAsync(ProductType productType)
        {
            await _productTypeDao.InsertAsync(productType).ConfigureAwait(false);
        }

        public async Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            await _shoppingListDao.UpdateAsync(shoppingList).ConfigureAwait(false);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productDao.UpdateAsync(product).ConfigureAwait(false);
        }

        public async Task DeleteProductWithChildrenAsync(Product product)
        {
            await _productDao.DeleteAsync(product).ConfigureAwait(false);
            await _productTypeDao.DeleteProductTypeWithoutFavouriteAsync(product.ProductType).ConfigureAwait(false);
        }

        public async Task DeleteProductType(ProductType productType)
        {
            await _productTypeDao.DeleteProductTypeAsync(productType).ConfigureAwait(false);
        }

        public async Task DeleteShoppingListAsync(ShoppingList shoppingList)
        {
            await _shoppingListDao.DeleteAsync(shoppingList).ConfigureAwait(false);
        }

        public async Task DeleteShoppingListWithProductsAsync(ShoppingList shoppingList)
        {
            if (shoppingList.Products != null && shoppingList.Products.Any())
            {
                foreach (Product product in shoppingList.Products)
                {
                    await DeleteProductWithChildrenAsync(product).ConfigureAwait(false);
                }
            }

            await DeleteShoppingListAsync(shoppingList).ConfigureAwait(false);
        }
    }
}