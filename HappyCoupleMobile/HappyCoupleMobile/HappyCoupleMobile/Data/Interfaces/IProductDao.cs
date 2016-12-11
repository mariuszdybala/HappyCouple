using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data
{
    public interface IProductDao : IBaseDao<Product>
    {
        Task<IList<Product>> GetAllProductsForShoppingListAsync(int shoppingListId);
    }
}