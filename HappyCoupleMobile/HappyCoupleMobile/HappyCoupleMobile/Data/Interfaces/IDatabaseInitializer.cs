using System.Threading.Tasks;
using SQLite.Net;

namespace HappyCoupleMobile.Data
{
    public interface IDatabaseInitializer
    {
        Task<CreateTablesResult> CreateTableAsync<T>() where T : class;
        Task EnsureAllTableExistsAsync();
        Task CleanAllTablesAsync();
        Task DropAllTablesAsync();
        Task FullDatabaseInitializeAsync();
    }
}