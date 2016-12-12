using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Data
{
    public class UserDao : BaseDao<User>, IUserDao
    {
        public UserDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
        {
        }

        public async Task<User> GetUserByInternalId(int internalId)
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<User>().Where(x => x.InternalId == internalId).FirstOrDefaultAsync();
        }

        public async Task<User> GetAdminUser()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<User>().Where(x => x.IsAdmin).FirstOrDefaultAsync();

        }
    }
}