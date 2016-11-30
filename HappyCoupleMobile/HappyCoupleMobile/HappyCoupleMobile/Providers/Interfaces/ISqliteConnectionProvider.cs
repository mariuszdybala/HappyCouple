using SQLite.Net.Async;

namespace HappyCoupleMobile.Providers.Interfaces
{
    public interface ISqliteConnectionProvider
    {
        SQLiteAsyncConnection GetConnection();
    }
}