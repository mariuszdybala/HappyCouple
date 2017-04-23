﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;

namespace HappyCoupleMobile.Services.Interfaces
{
    public interface IProductServices
    {
        Task<IList<ProductType>> GetAllProductTypesAync();
        Task<Dictionary<string, IList<Product>>> GetFavouriteTaskProductTypesWithProductsAsync();
        ProductVm CreateProductVm(string name, string comment, string quantity, ProductType productType, User user);
    }
}