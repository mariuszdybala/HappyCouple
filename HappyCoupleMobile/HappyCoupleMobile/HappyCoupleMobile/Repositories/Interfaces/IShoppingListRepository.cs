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
        Task<IList<Product>> GetAllProductsWithChildrenAsync();
        Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId);

        Task<IList<ProductType>> GetAllProductTypesPrimary();
        Task<IList<ProductType>> GetAllProductTypesFavorite();


        Task InsertShoppingListAsync(ShoppingList shoppingList);
        Task InsertProductAsync(Product product);
        Task InsertProductTypeAsync(ProductType productType);


        Task UpdateShoppingListAsync(ShoppingList shoppingList);
        Task UpdateProductAsync(Product product);


        Task DeleteProductWithChildrenAsync(Product product);
        Task DeleteShoppingListAsync(ShoppingList shoppingList);
        Task DeleteShoppingListWithProductsAsync(ShoppingList shoppingList);
    }
}