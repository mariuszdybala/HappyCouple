using System.Threading.Tasks;

namespace HappyCoupleMobile.Migrations
{
	public interface IMigration
	{
		Task ExecuteMigrationAsync();
	}
}
