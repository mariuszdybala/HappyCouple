using System.Threading.Tasks;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Migrations;
using HappyCoupleMobile.Resources;
using HappyCoupleMobile.Services.Interfaces;

namespace HappyCoupleMobile.Services
{
	public class StartApplicationService : IStartApplicationService
	{
		private readonly IDatabaseInitializer _databaseInitializer;
		private readonly IConfigurationDao _configurationDao;
		private readonly IMigrator _migrator;
		private readonly ISimpleAuthService _simpleAuthService;

		public StartApplicationService(IDatabaseInitializer databaseInitializer, IConfigurationDao configurationDao, IMigrator migrator, ISimpleAuthService simpleAuthService)
		{
			_databaseInitializer = databaseInitializer;
			_configurationDao = configurationDao;
			_migrator = migrator;
			_simpleAuthService = simpleAuthService;
		}

		public async Task InitApplicationAsync()
		{
			await InitDatabase();
			await RunMigrations();
			await LogUser();
		}

		private async Task RunMigrations()
		{
			await _migrator.ExecuteMigrationsAsync();
		}

		private async Task InitDatabase()
		{
			await _databaseInitializer.EnsureAllTableExistsAsync();

			var databaseInitialized = await _configurationDao.GetBoolAsync(ConfigurationKeys.DatabaseInitialized);
			bool firstDatabaseInitialization = !databaseInitialized.HasValue || !databaseInitialized.Value;
			if (firstDatabaseInitialization)
			{
				await _databaseInitializer.FullDatabaseInitializeAsync();
				await _configurationDao.SaveOrUpdateBoolAsync(ConfigurationKeys.DatabaseInitialized, true);
			}
		}

		private async Task LogUser()
		{
			await _simpleAuthService.LogIn();
		}
	}
}
