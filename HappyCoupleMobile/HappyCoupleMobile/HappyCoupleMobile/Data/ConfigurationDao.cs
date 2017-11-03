using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Data
{
	public class ConfigurationDao : BaseDao<Configuration>, IConfigurationDao
	{
		public ConfigurationDao(ISqliteConnectionProvider sqliteConnectionProvider) : base(sqliteConnectionProvider)
		{
		}

		private async Task<Configuration> GetConfigurationAsync(string key)
		{
			SQLiteAsyncConnection connection = GetConnection();
			var result = await connection.QueryAsync<Configuration>("SELECT * FROM Configuration WHERE key = ?", key).ConfigureAwait(false);
			return result.FirstOrDefault();
		}

		private async Task<string> GetStringAsync(string key)
		{
			Configuration configuration = await GetConfigurationAsync(key).ConfigureAwait(false);
			return configuration?.Value;
		}

		public async Task<bool?> GetBoolAsync(string key)
		{
			var value = await GetStringAsync(key).ConfigureAwait(false);
			return string.IsNullOrWhiteSpace(value) ? (bool?)null : bool.Parse(value);
		}

		public async Task<int?> GetIntAsync(string key)
		{
			var value = await GetStringAsync(key).ConfigureAwait(false);
			return string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value);
		}

		public async Task SaveOrUpdateBoolAsync(string key, bool value)
		{
			await SaveOrUpdateStringAsync(key, value.ToString());
		}

		public async Task SaveOrUpdateIntAsync(string key, int value)
		{
			await SaveOrUpdateStringAsync(key, value.ToString(CultureInfo.InvariantCulture)).ConfigureAwait(false);
		}

		private async Task SaveOrUpdateStringAsync(string key, string value)
		{
			var configuration = await GetConfigurationAsync(key).ConfigureAwait(false);

			if (configuration == null)
			{
				await InsertAsync(new Configuration { Key = key, Value = value }).ConfigureAwait(false);
			}
			else
			{
				configuration.Value = value;
				await UpdateAsync(configuration).ConfigureAwait(false);
			}
		}
	}
}
