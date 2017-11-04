using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;

namespace HappyCoupleMobile.Repositories
{
	public class ShoppingListRepository : IShoppingListRepository
	{
		private readonly IShoppingListDao _shoppingListDao;
		private readonly IProductDao _productDao;
		private readonly IProductTypeDao _productTypeDao;

		public ShoppingListRepository(IShoppingListDao shoppingListDao, IProductDao productDao,
			IProductTypeDao productTypeDao)
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

		public async Task<IList<Product>> GetAllFavouriteProductsWithChildrenAsync(int productTypeId)
		{
			return await _productDao.GetAllFavouriteProductsWithChildrenForTypeAsync(productTypeId);
		}

		public async Task<IList<Product>> GetAllProductsWithChildrenAsync()
		{
			return await _productDao.GetWithChildrenAsync().ConfigureAwait(false);
		}

		public async Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId)
		{
			return await _productDao.GetAllProductsForShoppingListAsync(shoppingListId).ConfigureAwait(false);
		}

		public async Task<IList<ProductType>> GetAllProductTypesAsync()
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

		public async Task DeleteProductAsync(Product product)
		{
			if (product.IsFavourite)
			{
				throw new InvalidOperationException("Attempt to delete product created as favourite");
			}
			await _productDao.DeleteAsync(product).ConfigureAwait(false);
		}

		public async Task DeleteFavouriteProductAsync(Product product)
		{
			if (product.ShoppingListId.HasValue)
			{
				throw new InvalidOperationException("Attempt to delete favourite product assignet to shopping list");
			}
			await _productDao.DeleteAsync(product);
		}

		public async Task DeleteShoppingListAsync(ShoppingList shoppingList)
		{
			await _shoppingListDao.DeleteAsync(shoppingList).ConfigureAwait(false);
		}

		public async Task DeleteShoppingListWithProductsAsync(ShoppingList shoppingList)
		{
			if (shoppingList.Products != null && shoppingList.Products.Any())
			{
				foreach (Product product in shoppingList.Products.Where(x => !x.IsFavourite))
				{
					await DeleteProductAsync(product).ConfigureAwait(false);
				}
			}

			await DeleteShoppingListAsync(shoppingList).ConfigureAwait(false);
		}

		public async Task<ProductType> GetProductTypeByTypeNameAsync(string name)
		{
			return await _productTypeDao.GetProductTypeByTypeNameAsync(name);
		}
	}
}
