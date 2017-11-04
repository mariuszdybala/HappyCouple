using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Repositories.Interfaces
{
    public interface IShoppingListRepository
    {
        Task<IList<ShoppingList>> GetAllShoppingListAsync();
        Task<IList<ShoppingList>> GetAllShoppingListWithProductsAsync();
        Task<IList<Product>> GetAllProductsAsync();
        Task<IList<Product>> GetAllFavouriteProductsWithChildrenAsync(int productTypeID);
        Task<IList<Product>> GetAllProductsWithChildrenAsync();
        Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId);

        Task<IList<ProductType>> GetAllProductTypesAsync();
        Task<ProductType> GetProductTypeByTypeNameAsync(string name);

        Task InsertShoppingListAsync(ShoppingList shoppingList);
        Task InsertProductAsync(Product product);
        Task InsertProductWithChildrenAsync(Product product);
        Task InsertProductTypeAsync(ProductType productType);


        Task UpdateShoppingListAsync(ShoppingList shoppingList);
        Task UpdateProductAsync(Product product);


        Task DeleteProductAsync(Product product);
        Task DeleteFavouriteProductAsync(Product product);
        Task DeleteShoppingListAsync(ShoppingList shoppingList);
        Task DeleteShoppingListWithProductsAsync(ShoppingList shoppingList);
    }
}