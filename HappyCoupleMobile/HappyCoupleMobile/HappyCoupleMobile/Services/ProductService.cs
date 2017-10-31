using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.VM;

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

        public async Task<Dictionary<string, IList<Product>>> GetFavouriteTaskProductTypesWithProductsAsync()
        {
            var productTypesDictionary = new Dictionary<string,IList<Product>>();

            var allProductTypes = await _shoppingListRepository.GetAllProductTypesAsync();

            var allFavouriteProducts = await _shoppingListRepository.GetAllFavouriteProductsWithChildrenAsync();

            //TODO Change to Linq.GroupBy
            foreach (var productType in allProductTypes)
            {
                var products = allFavouriteProducts.Where(x => x.ProductTypeId == productType.Id).ToList();
                productTypesDictionary.Add(productType.Type, products);
            }

            return productTypesDictionary;
        }

        public ProductVm CreateProductVm(string name, string comment, int quantity, ProductType productType, User user)
        {
            var product = new Product();

            product.Name = name;
            product.Comment = comment;
            product.Quantity = quantity;
            product.AddDate = DateTime.UtcNow;
            product.AddedById = user.Id;
            product.ProductType = productType;
            product.ProductTypeId = productType.Id;

            return new ProductVm(product);
        }
	    
	    public ProductVm CreateProductVm(string name, string comment)
	    {
		    var product = new Product();
		    product.Name = name;
		    product.Comment = comment;
		    return new ProductVm(product);
	    }
    }

}