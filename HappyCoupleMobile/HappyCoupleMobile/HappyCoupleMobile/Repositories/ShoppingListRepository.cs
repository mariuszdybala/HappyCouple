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

        public ShoppingListRepository(IShoppingListDao shoppingListDao, IProductDao productDao)
        {
            _shoppingListDao = shoppingListDao;
            _productDao = productDao;
        }

        public async Task<IList<ShoppingList>> GetAllShoppingListAsync()
        {
            return await _shoppingListDao.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await _productDao.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId)
        {
            return await _productDao.GetAllProductsForShoppingListAsync(shoppingListId).ConfigureAwait(false);
        }

        public async Task InsertShoppingListAsync(ShoppingList shoppingList)
        {
            await _shoppingListDao.InsertAsync(shoppingList).ConfigureAwait(false);
        }

        public async Task InsertProductAsync(Product product)
        {
            await _productDao.InsertAsync(product).ConfigureAwait(false);
        }

        public async Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            await _shoppingListDao.UpdateAsync(shoppingList).ConfigureAwait(false);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productDao.UpdateAsync(product).ConfigureAwait(false);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _productDao.DeleteAsync(product).ConfigureAwait(false);
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
                    await DeleteProductAsync(product).ConfigureAwait(false);
                }
            }

            await DeleteShoppingListAsync(shoppingList).ConfigureAwait(false);
        }
    }
}