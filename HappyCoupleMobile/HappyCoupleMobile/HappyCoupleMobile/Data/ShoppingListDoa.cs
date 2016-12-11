using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;

namespace HappyCoupleMobile.Data
{
    public class ShoppingListDao : BaseDao<ShoppingList>, IShoppingListDao
    {
        public ShoppingListDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
        {
        }
    }
}