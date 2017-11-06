using System.Collections.ObjectModel;
using System.Linq;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Custom
{
	public class GroupedShoppingLists : ObservableCollection<ShoppingList>
	{
		public ShoppingListStatus ShoppingListStatus { get; }

		public GroupedShoppingLists(IGrouping<ShoppingListStatus, ShoppingList> group) : base(group)
		{
			ShoppingListStatus = group.Key;
		}
	}
}
