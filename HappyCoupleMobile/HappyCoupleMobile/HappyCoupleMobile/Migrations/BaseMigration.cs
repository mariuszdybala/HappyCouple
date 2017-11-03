using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Migrations
{
	public abstract class BaseMigration : IMigration
	{
		private readonly ISqliteConnectionProvider _sqliteConnectionProvider = SimpleIoc.Default.GetInstance<ISqliteConnectionProvider>();

		protected SQLiteAsyncConnection GetConnection()
		{
			return _sqliteConnectionProvider.GetConnection();
		}

		public abstract Task ExecuteMigrationAsync();
	}
}
