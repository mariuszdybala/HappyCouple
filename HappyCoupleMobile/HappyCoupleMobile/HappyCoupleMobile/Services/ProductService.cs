using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services.Interfaces;

namespace HappyCoupleMobile.Services
{
    public class ProductService : IProductServices
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ProductService(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<IList<ProductType>> GetAllProductTypesAync()
        {
            return await _shoppingListRepository.GetAllProductTypesAsync();
        }

        public async Task<Dictionary<ProductType, IList<Product>>> GetFavouriteTaskProductTypesWithProductsAsync()
        {
            var productTypesDictionary = new Dictionary<ProductType,IList<Product>>();

            var allProductTypes = await _shoppingListRepository.GetAllProductTypesAsync();

            var allFavouriteProducts = await _shoppingListRepository.GetAllFavouriteProductsWithChildrenAsync();
            //TODO Change to Linq.GroupBy
            foreach (var productType in allProductTypes)
            {
                var products = allFavouriteProducts.Where(x => x.ProductTypeId == productType.Id).ToList();
                productTypesDictionary.Add(productType, products);
            }

            return productTypesDictionary;
        }
    }

}