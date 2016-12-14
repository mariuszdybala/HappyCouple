using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data
{
    public interface IProductTypeDao : IBaseDao<ProductType>
    {
        Task DeleteProductTypeWithoutFavourite(ProductType productType);
        Task DeleteProductType(ProductType productType);

        Task<IList<ProductType>> GetAllProductTypesAddedByUser();
        Task<IList<ProductType>> GetAllProductTypesPrimary();
        Task<IList<ProductType>> GetAllProductTypesFavorite();
    }
}