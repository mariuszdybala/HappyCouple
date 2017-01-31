using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;

namespace HappyCoupleMobile.Services
{
    public class ProductService : IProductService
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ProductService(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<IList<ProductType>> GetPrimaryProductTypes()
        {
            return await _shoppingListRepository.GetAllProductTypesPrimary();
        }
    }
}