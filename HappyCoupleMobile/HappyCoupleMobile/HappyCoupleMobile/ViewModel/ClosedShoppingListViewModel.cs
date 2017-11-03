using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Providers.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.ViewModel.Abstract;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
	public class ClosedShoppingListViewModel : BaseHappyViewModel
	{
		private readonly IAlertsAndNotificationsProvider _alertsAndNotificationsProvider;
		
		public Command DeleteListCommand => new Command(OnDeleteList);
		
		private ShoppingListVm _shoppingList;
		private ObservableCollection<GroupedProductList> _groupedProducts;

		public ShoppingListVm ShoppingList
		{
			get => _shoppingList;
			set => Set(ref _shoppingList, value);
		}

		public ObservableCollection<GroupedProductList> GroupedProducts
		{
			get => _groupedProducts;
			set => Set(ref _groupedProducts, value);
		}

		public ClosedShoppingListViewModel(ISimpleAuthService simpleAuthService, IAlertsAndNotificationsProvider alertsAndNotificationsProvider) : base(simpleAuthService)
		{
			_alertsAndNotificationsProvider = alertsAndNotificationsProvider;
			
			RegisterCommand();
		}

		protected override async Task OnNavigateTo(IMessageData message)
		{
			ShoppingList = (ShoppingListVm)message.GetValue(MessagesKeys.ShoppingListKey);

			if (ShoppingList == null)
			{
				return;
				
			}
			ReloadProductsGroups();
		}
		
		private void ReloadProductsGroups()
		{
			var groupedData = ShoppingList.Products.OrderBy(x => x.ProductType.Type)
				.GroupBy(x => x.ProductType, new ProductTypeEqualityComparer())
				.Select(x => new GroupedProductList(x))
				.ToList();

			GroupedProducts = new ObservableCollection<GroupedProductList>(groupedData);
		}
		
		private void RegisterCommand()
		{
			RegisterNavigateToMessage(this);
		}
		
		private void OnDeleteList()
		{
			_alertsAndNotificationsProvider.ShowAlertWithConfirmation("na pewno tego chcesz?", "Lista zostanie bezpowrotnie usunięta",
				async (confirmed) => await DeleteListConfiramtion(confirmed));
		}

		private async Task DeleteListConfiramtion(bool confirmed)
		{
			if (!confirmed)
			{
				return;
			}

			await SendFeedbackMessage<ShoppingsViewModel>(async (shoppingsViewModel) => await shoppingsViewModel.DeleteListConfiramtion(true, ShoppingList));
			await NavigateBack();
		}
	}
}
