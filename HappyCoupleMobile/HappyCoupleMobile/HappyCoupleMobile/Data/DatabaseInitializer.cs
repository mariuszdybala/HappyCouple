using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ISqliteConnectionProvider _sqliteConnectionProvider;

        public DatabaseInitializer(ISqliteConnectionProvider sqliteConnectionProvider)
        {
            _sqliteConnectionProvider = sqliteConnectionProvider;
        }


        public async Task<CreateTablesResult> CreateTableAsync<T>() where T : class
        {
            SQLiteAsyncConnection connection = _sqliteConnectionProvider.GetConnection();
            return await connection.CreateTableAsync<T>().ConfigureAwait(false);
        }

        public async Task EnsureAllTableExistsAsync()
        {
            SQLiteAsyncConnection connection = _sqliteConnectionProvider.GetConnection();

            await connection.CreateTableAsync<ShoppingList>().ConfigureAwait(false);
            await connection.CreateTableAsync<Product>().ConfigureAwait(false);
            await connection.CreateTableAsync<User>().ConfigureAwait(false);
	        await connection.CreateTableAsync<ProductType>().ConfigureAwait(false);
	        await connection.CreateTableAsync<Configuration>().ConfigureAwait(false);
        }

        public async Task CleanAllTablesAsync()
        {
            SQLiteAsyncConnection connection = _sqliteConnectionProvider.GetConnection();

            await connection.DeleteAllAsync<ShoppingList>().ConfigureAwait(false);
            await connection.DeleteAllAsync<Product>().ConfigureAwait(false);
            await connection.DeleteAllAsync<User>().ConfigureAwait(false);
            await connection.DeleteAllAsync<ProductType>().ConfigureAwait(false);
	        await connection.DeleteAllAsync<Configuration>().ConfigureAwait(false);
        }

        public async Task DropAllTablesAsync()
        {
            SQLiteAsyncConnection connection = _sqliteConnectionProvider.GetConnection();

            await connection.DropTableAsync<ShoppingList>().ConfigureAwait(false);
            await connection.DropTableAsync<Product>().ConfigureAwait(false);
            await connection.DropTableAsync<User>().ConfigureAwait(false);
            await connection.DropTableAsync<ProductType>().ConfigureAwait(false);
	        await connection.DropTableAsync<Configuration>().ConfigureAwait(false);
        }

        public async Task FullDatabaseInitializeAsync()
        {
            await DropAllTablesAsync().ConfigureAwait(false);
            await EnsureAllTableExistsAsync().ConfigureAwait(false);
        }
    }
}
