using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data
{
    public interface IProductTypeDao : IBaseDao<ProductType>
    {
        Task DeleteProductTypeWithoutFavouriteAsync(ProductType productType);
        Task DeleteProductTypeWithoutPrimaryAsync(ProductType productType);
        Task DeleteProductTypeAsync(ProductType productType);

        Task<IList<ProductType>> GetAllProductTypesAddedByUserAsync();
        Task<IList<ProductType>> GetAllProductTypesPrimaryAsync();
        Task<IList<ProductType>> GetAllProductTypesFavoriteAsync();
    }
}