using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.VM;

namespace HappyCoupleMobile.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ISimpleAuthService _simpleAuthService;
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListService(ISimpleAuthService simpleAuthService, IShoppingListRepository shoppingListRepository)
        {
            _simpleAuthService = simpleAuthService;
            _shoppingListRepository = shoppingListRepository;
        }

	    public async Task<IList<ShoppingListVm>> GetAllShoppingListWithProductsAsync(ShoppingListStatus status)
	    {
		    var shoppingList = await _shoppingListRepository.GetAllShoppingListWithProductsAsync(status);

		    var products = await _shoppingListRepository.GetAllProductsAsync();
		    
		    return shoppingList.Select(x=> new ShoppingListVm(x)).ToList();
	    }

	    public async Task InsertShoppingListAsync(ShoppingListVm shoppingList)
	    {
		    await _shoppingListRepository.InsertShoppingListAsync(shoppingList.ShoppingList);
	    }

	    public async Task UpdateShoppingListAsync(ShoppingListVm shoppingList)
	    {
		    if (shoppingList == null)
		    {
			    return;
		    }

		    await _shoppingListRepository.UpdateShoppingListAsync(shoppingList.ShoppingList);
	    }

	    public async Task<IList<ProductType>> GetAllProductTypesAync()
	    {
		    return await _shoppingListRepository.GetAllProductTypesAsync();
	    }

	    public async Task InsertProductsAsync(IList<ProductVm> products)
	    {
		    if (products == null)
		    {
			    return;
		    }

		    foreach (var product in products)
		    {
			    await InsertProductAsync(product);
		    }
	    }

	    public async Task UpdateProductsAsync(IList<ProductVm> products)
	    {
		    if (products == null)
		    {
			    return;
		    }
		    
		    foreach (var product in products)
		    {
			   await _shoppingListRepository.UpdateProductAsync(product.ProductModel);
		    }
	    }

	    public async Task DeleteFavouriteProductAsync(ProductVm product)
	    {
		    if (product == null)
		    {
			    return;
		    }

		    await _shoppingListRepository.DeleteFavouriteProductAsync(product.ProductModel);
	    }

	    public async Task<IList<ProductVm>> GetAllFavouriteProductsForTypeAsync(int typeId)
	    {
		    var favouriteProducts = await _shoppingListRepository.GetAllFavouriteProductsWithChildrenAsync(typeId);

		    return favouriteProducts.Select(x => new ProductVm(x)).ToList();
	    }

	    public async Task DeleteShoppingListAsync(ShoppingListVm shoppingList)
	    {
		    if (shoppingList == null)
		    {
			    return;
		    }

		    foreach (var product in shoppingList.Products)
		    {
			    await DeleteProductAsync(product);
		    }

		    await _shoppingListRepository.DeleteShoppingListAsync(shoppingList.ShoppingList);
	    }

	    public async Task UpdateProductAsync(ProductVm product)
	    {
		    if (product == null)
		    {
			    return;
		    }
		    
		    await _shoppingListRepository.UpdateProductAsync(product.ProductModel);
	    }

	    public async Task UpdateFavouriteProductAsync(ProductVm product)
	    {
		    if (product == null)
		    {
			    return;
		    }
		    
		    await _shoppingListRepository.UpdateFavouriteProductAsync(product.ProductModel);
	    }

	    public async Task DeleteProductAsync(ProductVm product)
	    {
		    if (product == null)
		    {
			    return;
		    }

		    await _shoppingListRepository.DeleteProductAsync(product.ProductModel);
	    }

	    public async Task InsertProductAsync(ProductVm product)
	    {
		    if (product == null)
		    {
			    return;
		    }

		    await _shoppingListRepository.InsertProductAsync(product.ProductModel);
	    }
	    
	    public async Task InsertFavouriteProductAsync(ProductVm product)
	    {
		    if (product == null)
		    {
			    return;
		    }

		    await _shoppingListRepository.InsertFavouriteProductAsync(product.ProductModel);
	    }
    }
}