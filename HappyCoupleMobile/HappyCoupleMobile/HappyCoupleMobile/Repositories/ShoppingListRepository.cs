using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.VM;

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

		public async Task<IList<ShoppingList>> GetAllShoppingListWithProductsAsync(ShoppingListStatus status)
		{
			return status == ShoppingListStatus.Active
				? await _shoppingListDao.GetActiveShoppingListAsync()
				: await _shoppingListDao.GetClosedShoppingListAsync();
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
			CheckOperationOnNormalProductIsCorrect(product, OperationMode.InsertNew);
			
			await _productDao.InsertAsync(product).ConfigureAwait(false);
		}
		
		public async Task InsertFavouriteProductAsync(Product product)
		{
			CheckOperationOnFavouriteProductIsCorrect(product, OperationMode.InsertNew);
			
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
			CheckOperationOnNormalProductIsCorrect(product, OperationMode.Update);
			
			await _productDao.UpdateAsync(product).ConfigureAwait(false);
		}
		
		public async Task UpdateFavouriteProductAsync(Product product)
		{
			CheckOperationOnFavouriteProductIsCorrect(product, OperationMode.Update);
			
			await _productDao.UpdateAsync(product).ConfigureAwait(false);
		}
		
		public async Task DeleteProductAsync(Product product)
		{
			CheckOperationOnNormalProductIsCorrect(product, OperationMode.Delete);
			await _productDao.DeleteAsync(product).ConfigureAwait(false);
		}

		public async Task DeleteFavouriteProductAsync(Product product)
		{
			CheckOperationOnFavouriteProductIsCorrect(product, OperationMode.Delete);
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

		private void CheckOperationOnFavouriteProductIsCorrect(Product product, OperationMode operationMode)
		{
			CheckOperationOnProductIsCorrect(true, product, operationMode);
		}
		
		private void CheckOperationOnNormalProductIsCorrect(Product product, OperationMode operationMode)
		{
			CheckOperationOnProductIsCorrect(false, product, operationMode);
		}
		
		private void CheckOperationOnProductIsCorrect(bool hasToBeFavourite, Product product, OperationMode operationMode)
		{
			if (hasToBeFavourite && product.ShoppingListId.HasValue)
			{
				throw new InvalidOperationException($"Attempt to {operationMode.ToString()} favourite product assignet to shopping list");
			}
			
			if (!hasToBeFavourite && product.IsFavourite)
			{
				throw new InvalidOperationException($"Attempt to {operationMode.ToString()} product created as favourite");
			}
		}
	}
}
