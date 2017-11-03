using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Providers.Interfaces;
using HappyCoupleMobile.Resources;
using SQLiteNetExtensions.Extensions;

namespace HappyCoupleMobile.Migrations
{
	public class Migrator : IMigrator
	{
		private readonly IConfigurationDao _configurationDao;
		private readonly IAssemblyInfoProvider _assemblyInfoProvider;

		public Migrator(IConfigurationDao configurationDao, IAssemblyInfoProvider assemblyInfoProvider)
		{
			_configurationDao = configurationDao;
			_assemblyInfoProvider = assemblyInfoProvider;
		}

		public async Task ExecuteMigrationsAsync()
		{
			var lastExecutedId = await _configurationDao.GetIntAsync(ConfigurationKeys.LastMigrationId).ConfigureAwait(false);

			await ExecuteMigrationIfNeeded(lastExecutedId).ConfigureAwait(false);
		}

		private async Task ExecuteMigrationIfNeeded(int? lastMigrationId)
		{
			int newLastMigrationId = 0;
			int lastMigrationIdValue = lastMigrationId ?? 0;

			var definedMigrations = GetDefinedMigrationsOrdered();

			foreach (var migration in definedMigrations.Where(m => m.Key > lastMigrationIdValue))
			{
				var migrationInstance = Activator.CreateInstance(migration.Value) as IMigration;
				if (migrationInstance != null)
				{
					await migrationInstance.ExecuteMigrationAsync().ConfigureAwait(false);
					newLastMigrationId = migration.Key;
				}
			}

			if (newLastMigrationId > lastMigrationIdValue)
			{
				await _configurationDao.SaveOrUpdateIntAsync(ConfigurationKeys.LastMigrationId, newLastMigrationId).ConfigureAwait(false);
			}
		}

		private IOrderedEnumerable<KeyValuePair<int, Type>> GetDefinedMigrationsOrdered()
		{
			var excluded = new List<Type> { typeof(BaseMigration) };

			return _assemblyInfoProvider.GetDefinedMigrationTypes()
				.Except(excluded)
				.ToDictionary(x => x.GetAttribute<MigrationAttribute>().MigrationId, x => x)
				.OrderBy(x => x.Key);
		}
	}
}
