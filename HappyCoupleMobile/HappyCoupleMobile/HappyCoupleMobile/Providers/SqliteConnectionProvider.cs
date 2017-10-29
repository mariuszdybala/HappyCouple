using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using Xamarin.Forms;

namespace HappyCoupleMobile.Providers
{
    public class SqliteConnectionProvider : ISqliteConnectionProvider
    {
        private readonly ISystemInfoProvider _systemInfoProvider;
        private readonly ISQLitePlatform _sqLitePlatform;

        private SQLiteAsyncConnection _sqLiteAsyncConnection;

        public SqliteConnectionProvider(ISystemInfoProvider systemInfoProvider)
        {
            _systemInfoProvider = systemInfoProvider;
            _sqLitePlatform = _systemInfoProvider.SqLitePlatform;
        }
        public SQLiteAsyncConnection GetConnection()
        {
            if (_sqLiteAsyncConnection == null)
            {
                _sqLiteAsyncConnection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(_sqLitePlatform, new SQLiteConnectionString(_systemInfoProvider.SqliteDatabasePath, true)));
            }

            return _sqLiteAsyncConnection;
        }
    }
}