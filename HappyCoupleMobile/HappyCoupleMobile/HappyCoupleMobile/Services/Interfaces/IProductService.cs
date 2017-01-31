using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Services
{
    public interface IProductService
    {
        Task<IList<ProductType>> GetPrimaryProductTypes();
    }
}