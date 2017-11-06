using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Notification.Interfaces;
using HappyCoupleMobile.Providers.Interfaces;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class ShoppingsViewModel : BaseHappyViewModel
    {
        private readonly IShoppingListService _shoppingListService;
	    private readonly ISimpleAuthService _authService;
	    private readonly INotificationManager _notificationManager;
	    private readonly IAlertsAndNotificationsProvider _alertsAndNotificationsProvider;

	    private ObservableCollection<ShoppingListVm> _closedShoppingLists;
        private ObservableCollection<ShoppingListVm> _activeShoppingLists;

	    public Command SettingsTappedCommand => new Command(OnSettingsTapped);

	    public Command AddNewListCommand { get; set; }
        public Command<string> CreateNewListCommand { get; set; }
        public Command<ShoppingListVm> DeleteListCommand { get; set; }
        public Command<ShoppingListVm> AddProductToListCommand { get; set; }
        public Command<ShoppingListVm> CloseListCommand { get; set; }
        public Command<ShoppingListVm> EditListCommand { get; set; }

        public ObservableCollection<ShoppingListVm> ActiveShoppingLists
        {
            get => _activeShoppingLists;
	        set => Set(ref _activeShoppingLists, value);
        }

        public ObservableCollection<ShoppingListVm> ClosedShoppingLists
        {
            get => _closedShoppingLists;
	        set => Set(ref _closedShoppingLists, value);
        }

        public ShoppingsViewModel(ISimpleAuthService simpleAuthService, IShoppingListService shoppingListService, ISimpleAuthService authService,
                                    INotificationManager notificationManager,  IAlertsAndNotificationsProvider alertsAndNotificationsProvider) : base(simpleAuthService)
        {
            _shoppingListService = shoppingListService;
	        _authService = authService;
	        _notificationManager = notificationManager;
	        _alertsAndNotificationsProvider = alertsAndNotificationsProvider;
	        RegisterCommand();

            ActiveShoppingLists = new ObservableCollection<ShoppingListVm>();
        }

        private void RegisterCommand()
        {
            AddNewListCommand = new Command(OnAddNewListCommand);
            DeleteListCommand = new Command<ShoppingListVm>(OnDeleteList);
            AddProductToListCommand = new Command<ShoppingListVm>(async (shoppingList) => await OnAddProductToList(shoppingList));
            CloseListCommand = new Command<ShoppingListVm>(async(shoppingList) => await OnCloseList(shoppingList));
            EditListCommand = new Command<ShoppingListVm>(async(shoppingList) => await OnEditList(shoppingList));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            await Task.Yield();
        }

        public async Task GetAllShoppingListsAndInitView()
        {
	        var activeShoppingList = await _shoppingListService.GetAllShoppingListWithProductsAsync(ShoppingListStatus.Active);
	        var closedShoppingList = await _shoppingListService.GetAllShoppingListWithProductsAsync(ShoppingListStatus.Closed);

            ActiveShoppingLists = new ObservableCollection<ShoppingListVm>(activeShoppingList);
            ClosedShoppingLists = new ObservableCollection<ShoppingListVm>(closedShoppingList);

            RaisePropertyChanged(nameof(ActiveShoppingLists));
            RaisePropertyChanged(nameof(ClosedShoppingLists));
        }

        private async Task OnEditList(ShoppingListVm shoppingList)
        {
	        if (shoppingList.Status == ShoppingListStatus.Active)
	        {
		        await NavigateToWithMessage<EditShoppingListView, EditShoppingListViewModel>(new BaseMessage<EditShoppingListViewModel>(MessagesKeys.ShoppingListKey, shoppingList));
	        }
	        else
	        {
		        await NavigateToWithMessage<ClosedShoppingListView, ClosedShoppingListViewModel>(new BaseMessage<ClosedShoppingListViewModel>(MessagesKeys.ShoppingListKey, shoppingList));
	        }
        }

	    private void OnDeleteList(ShoppingListVm shoppingList)
	    {
		    _alertsAndNotificationsProvider.ShowAlertWithConfirmation("na pewno tego chcesz?", "Lista zostanie bezpowrotnie usunięta",
			    async (confirmed) => await DeleteListConfiramtion(confirmed, shoppingList));
	    }

	    public async Task DeleteListConfiramtion(bool confirmed, ShoppingListVm shoppingList)
	    {
		    if (!confirmed)
		    {
			    return;
		    }

		    if (shoppingList.Status == ShoppingListStatus.Active)
		    {
			    ActiveShoppingLists.Remove(shoppingList);
		    }
		    else
		    {
			    ClosedShoppingLists.Remove(shoppingList);
		    }

		    await _shoppingListService.DeleteShoppingListAsync(shoppingList);

		    _alertsAndNotificationsProvider.ShowSuccessToast("Lista usunięta");
	    }

	    private async Task OnCloseList(ShoppingListVm shoppingList)
        {
	        if (shoppingList.IsListCompleted)
	        {
		        await CloseListConfiramtion(true, shoppingList);
		        return;
	        }

	        _alertsAndNotificationsProvider.ShowAlertWithConfirmation("mimo, że nie wszystkie produkty zostały zakupione.", "Lista zostanie zakmknięta",
		        async (confirmed) => await CloseListConfiramtion(confirmed, shoppingList));
        }

	    public async Task CloseListConfiramtion(bool confirmed, ShoppingListVm shoppingList)
	    {
		    if (confirmed)
		    {
			    await CloseList(shoppingList);
			    _alertsAndNotificationsProvider.ShowSuccessToast("Lista zamknięta");
		    }
	    }

	    private async Task CloseList(ShoppingListVm shoppingList)
	    {
		    shoppingList.Status = ShoppingListStatus.Closed;
		    shoppingList.CloseDate = DateTime.Now;

		    await _shoppingListService.UpdateShoppingListAsync(shoppingList);

		    ActiveShoppingLists.Remove(shoppingList);
		    ClosedShoppingLists.Add(shoppingList);
	    }

	    private async Task OnAddProductToList(ShoppingListVm shoppingList)
        {
	        var message = new BaseMessage<FavouriteProductTypeViewModel>(MessagesKeys.ShoppingListIdKey, shoppingList.Id);
	        await NavigateToWithMessage<FavouriteProductTypesView, FavouriteProductTypeViewModel>(message);
        }

        private void OnAddNewListCommand()
        {
	        _alertsAndNotificationsProvider.ShowActionSheet("wybierz sposób", "Utwórz nową listę", new List<ActionSheetItem>
	        {
		        new ActionSheetItem {Action = OnAddNewListManual, ButtonText = "Dodaj ręcznie"},
		        new ActionSheetItem {Action = OnAddNewListManual, ButtonText = "Na podstawie innej listy"}
	        });
        }

	    private void OnSettingsTapped()
	    {
		    _alertsAndNotificationsProvider.ShowActionSheet(string.Empty, "Ustawienia", new List<ActionSheetItem>
		    {
			    new ActionSheetItem {Action = async () => await EditFavouriteProductsList(), ButtonText = "Edytuj listę swoich produktów"},
		    });
	    }

	    private async Task EditFavouriteProductsList()
	    {
		    await NavigateToWithMessage<FavouriteProductTypesView, FavouriteProductTypeViewModel>(
			    new BaseMessage<FavouriteProductTypeViewModel>());
	    }

	    private void OnAddNewListManual()
	    {
		    _alertsAndNotificationsProvider.ShowAlertWithTextField("Wpisz swoją nazwę listy", "Nowa lista zakupów", Keyboard.Default, async(listName) => await AlertsAndNotificationsProviderOnAlertConfirmed(listName));
	    }

	    private async Task AlertsAndNotificationsProviderOnAlertConfirmed(string listName)
	    {
		    var newList = ShoppingListVm.CreateNewShoppingList(listName, _authService.Admin.Id);
		    await AddShoppingList(newList);
		    _alertsAndNotificationsProvider.ShowSuccessToast();
	    }

	    private async Task AddShoppingList(ShoppingListVm shoppingListVm)
	    {
		    ActiveShoppingLists.Add(shoppingListVm);
		    await _shoppingListService.InsertShoppingListAsync(shoppingListVm);
	    }

	    protected override async Task OnFeedback(IFeedbackMessage feedbackMessage)
	    {
		    var products = feedbackMessage.GetFirstOrDefaultProductsRange();

		    if (products == null || !products.Any())
		    {
			    return;
		    }

		    if (feedbackMessage.OperationMode == OperationMode.InsertNew)
		    {
			    var shoppingListId = feedbackMessage.GetInt(MessagesKeys.ShoppingListIdKey);
			    await AddProducts(products, shoppingListId);
		    }
		    else if (feedbackMessage.OperationMode == OperationMode.Update)
		    {
			    EditProducts(products);
		    }
	    }

	    private async Task AddProducts(IList<ProductVm> products, int? shoppingListId)
	    {
		    if (!shoppingListId.HasValue)
		    {
			    throw new ArgumentException("Added product hasn't got shopping list ID");
		    }

		    var shoppingList = ActiveShoppingLists.FirstOrDefault(x => x.Id == shoppingListId);
		    shoppingList.AddProducts(products);
		    await _shoppingListService.InsertProductsAsync(products);
		    await ShoppingListChanged(shoppingList);
	    }

	    private async Task ShoppingListChanged(ShoppingListVm shoppingList)
	    {
		    shoppingList.EditDate = DateTime.Now;
		    await _shoppingListService.UpdateShoppingListAsync(shoppingList);
	    }

	    private void EditProducts(IList<ProductVm> products)
	    {
		    foreach (var list in ActiveShoppingLists)
		    {
			    list.UpdateProducts(products);
		    }
		    
		    _shoppingListService.UpdateProductsAsync(products);
	    }
    }
}
