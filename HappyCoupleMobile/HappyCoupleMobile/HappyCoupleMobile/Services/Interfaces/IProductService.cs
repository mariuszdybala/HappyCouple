using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Services.Interfaces
{
    public interface IProductServices
    {
        Task<IList<ProductType>> GetAllProductTypesAync();
        Task<Dictionary<string, IList<Product>>> GetFavouriteTaskProductTypesWithProductsAsync();
    }
}