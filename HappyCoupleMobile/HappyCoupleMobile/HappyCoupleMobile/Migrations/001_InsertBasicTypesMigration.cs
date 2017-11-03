using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using SQLite.Net.Async;

namespace HappyCoupleMobile.Migrations
{
	[Migration(1)]
	public class InsertBasicTypesMigration : BaseMigration
	{
		public override async Task ExecuteMigrationAsync()
		{
			SQLiteAsyncConnection connection = GetConnection();

			IShoppingListRepository shoppingListRepository = SimpleIoc.Default.GetInstance<IShoppingListRepository>();

			await shoppingListRepository.InsertProductTypeAsync(GetProductType("Owoce", "Fruits"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Oliwa", "Olive"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Napoje", "Drink"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Warzywa", "Vege"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Rybka", "Fish"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Chleb", "Bread"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Mięso", "Meat"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Przyprawy", "Spice"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Nabiał", "Dairy"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Sypkie", "Grain"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Jedzenie", "FoodGeneral"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Alko", "Beer"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Rośliny", "Plant"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Kosmetyki", "Cosmetics"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Chemia", "Cleaning"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Higiena", "Hygiene"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Home&You", "Home"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Ciuchy", "Clothes"));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Leki", "Medicine"));
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
