using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Data
{
    public class ProductTypeDao : BaseDao<ProductType>, IProductTypeDao
    {
        public ProductTypeDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
        {
        }
    }
}