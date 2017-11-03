using System.Threading.Tasks;

namespace HappyCoupleMobile.Migrations
{
	public interface IMigrator
	{
		Task ExecuteMigrationsAsync();
	}
}
