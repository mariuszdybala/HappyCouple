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
        Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId);


        Task InsertShoppingListAsync(ShoppingList shoppingList);
        Task InsertProductAsync(Product product);


        Task UpdateShoppingListAsync(ShoppingList shoppingList);
        Task UpdateProductAsync(Product product);


        Task DeleteProductAsync(Product product);
        Task DeleteShoppingListAsync(ShoppingList shoppingList);
        Task DeleteShoppingListWithProductsAsync(ShoppingList shoppingList);
    }
}