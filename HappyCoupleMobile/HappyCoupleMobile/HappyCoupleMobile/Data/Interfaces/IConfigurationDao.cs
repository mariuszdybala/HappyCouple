using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Data.Interfaces
{
	public interface IConfigurationDao : IBaseDao<Configuration>
	{
		Task<bool?> GetBoolAsync(string key);
		Task<int?> GetIntAsync(string key);

		Task SaveOrUpdateBoolAsync(string key, bool value);
		Task SaveOrUpdateIntAsync(string key, int value);
	}
}
