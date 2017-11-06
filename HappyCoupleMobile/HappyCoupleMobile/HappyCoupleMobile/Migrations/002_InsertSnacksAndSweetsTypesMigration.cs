using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Migrations
{
	[Migration(2)]
	public class InsertSnacksAndSweetsTypesMigration : BaseMigration
	{
		public override async Task ExecuteMigrationAsync()
		{
			SQLiteAsyncConnection connection = GetConnection();

			IShoppingListRepository shoppingListRepository = SimpleIoc.Default.GetInstance<IShoppingListRepository>();

			await shoppingListRepository.InsertProductTypeAsync(GetProductType("Słodycze", "Sweets"));
			await shoppingListRepository.InsertProductTypeAsync(GetProductType("Przekąski", "Snacks"));
		}
		
		private ProductType GetProductType(string name, string iconResourceName)
		{
			return new ProductType
			{
				Type = name,
				IconName = iconResourceName
			};
		}
	}
}
