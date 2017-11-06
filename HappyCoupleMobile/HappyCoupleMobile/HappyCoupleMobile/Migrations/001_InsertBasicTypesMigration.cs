using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Enums;
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
			
			await shoppingListRepository.InsertProductTypeAsync(GetProductType("Słodycze", "Sweets", ProductGroup.Food));
			await shoppingListRepository.InsertProductTypeAsync(GetProductType("Przekąski", "Snacks", ProductGroup.Food));
			await shoppingListRepository.InsertProductTypeAsync(GetProductType("Owoce", "Fruits", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Oliwa", "Olive", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Napoje", "Drink", ProductGroup.Drinks));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Warzywa", "Vege", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Rybka", "Fish", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Chleb", "Bread", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Mięso", "Meat", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Przyprawy", "Spice", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Nabiał", "Dairy", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Sypkie", "Grain", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Jedzenie", "FoodGeneral", ProductGroup.Food));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Alko", "Beer", ProductGroup.Drinks));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Rośliny", "Plant", ProductGroup.Home));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Kosmetyki", "Cosmetics", ProductGroup.Cosmetics));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Chemia", "Cleaning", ProductGroup.Chemistry));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Higiena", "Hygiene", ProductGroup.Cosmetics));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Home&You", "Home", ProductGroup.Home));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Ciuchy", "Clothes", ProductGroup.Clothes));
            await shoppingListRepository.InsertProductTypeAsync(GetProductType("Leki", "Medicine", ProductGroup.Chemistry));
		}

		private ProductType GetProductType(string name, string iconResourceName, ProductGroup groupId)
		{
			return new ProductType
			{
				Type = name,
				IconName = iconResourceName,
				Group = groupId
			};
		}
	}
}
