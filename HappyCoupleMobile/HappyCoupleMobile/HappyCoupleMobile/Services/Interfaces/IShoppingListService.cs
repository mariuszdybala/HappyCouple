using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;

namespace HappyCoupleMobile.Services.Interfaces
{
    public interface IShoppingListService
    {
	    Task<IList<ShoppingListVm>> GetAllShoppingListWithProductsAsync(ShoppingListStatus status);
	    Task InsertShoppingListAsync(ShoppingListVm shoppingList);
	    Task UpdateShoppingListAsync(ShoppingListVm shoppingList);
	    
	    Task<IList<ProductType>> GetAllProductTypesAync();
	    
	    Task InsertProductsAsync(IList<ProductVm> products);
	    Task InsertFavouriteProductAsync(ProductVm product);
	    Task UpdateProductsAsync(IList<ProductVm> products);
	    Task UpdateProductAsync(ProductVm product);
	    Task UpdateFavouriteProductAsync(ProductVm product);
	    Task DeleteProductAsync(ProductVm product);
	    Task DeleteFavouriteProductAsync(ProductVm product);
	    
	    Task<IList<ProductVm>> GetAllFavouriteProductsForTypeAsync(int typeId);
	    Task DeleteShoppingListAsync(ShoppingListVm shoppingList);
    }
}