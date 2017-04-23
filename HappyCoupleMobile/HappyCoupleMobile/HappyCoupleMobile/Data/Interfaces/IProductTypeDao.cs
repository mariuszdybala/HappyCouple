using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data
{
    public interface IProductTypeDao : IBaseDao<ProductType>
    {
        Task<ProductType> GetProductTypeByTypeNameAsync(string name);
    }
}