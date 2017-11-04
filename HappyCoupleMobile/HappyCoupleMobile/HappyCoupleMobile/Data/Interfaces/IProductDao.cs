using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data.Interfaces
{
    public interface IProductDao : IBaseDao<Product>
    {
        Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId);
	    Task<IList<Product>> GetAllFavouriteProductsWithChildrenForTypeAsync(int productTypeId);
    }
}