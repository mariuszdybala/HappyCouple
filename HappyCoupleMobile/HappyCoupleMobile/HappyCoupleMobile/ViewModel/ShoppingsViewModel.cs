using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly INotificationManager _notificationManager;
	    private readonly IAlertsAndNotificationsProvider _alertsAndNotificationsProvider;

	    private ObservableCollection<ShoppingListVm> _closedShoppingLists;
        private ObservableCollection<ShoppingListVm> _activeShoppingLists;

        public Command AddNewListCommand { get; set; }
        public Command<string> CreateNewListCommand { get; set; }
        public Command<ShoppingListVm> DeleteListCommand { get; set; }
        public Command<ShoppingListVm> AddProductToListCommand { get; set; }
        public Command<ShoppingListVm> CloseListCommand { get; set; }
        public Command<ShoppingListVm> EditListCommand { get; set; }

        public ObservableCollection<ShoppingListVm> ActiveShoppingLists
        {
            get { return _activeShoppingLists; }
            set
            {
                Set(ref _activeShoppingLists, value);
            }
        }

        public ObservableCollection<ShoppingListVm> ClosedShoppingLists
        {
            get { return _closedShoppingLists; }
            set
            {
                Set(ref _closedShoppingLists, value);
            }
        }

        public ShoppingsViewModel(ISimpleAuthService simpleAuthService, IShoppingListService shoppingListService, IShoppingListRepository shoppingListRepository,
                                    INotificationManager notificationManager,  IAlertsAndNotificationsProvider alertsAndNotificationsProvider) : base(simpleAuthService)
        {
            _shoppingListService = shoppingListService;
            _shoppingListRepository = shoppingListRepository;
            _notificationManager = notificationManager;
	        _alertsAndNotificationsProvider = alertsAndNotificationsProvider;
	        RegisterCommand();

            ActiveShoppingLists = new ObservableCollection<ShoppingListVm>();
        }

        private void RegisterCommand()
        {
            AddNewListCommand = new Command(OnAddNewListCommand);
            DeleteListCommand = new Command<ShoppingListVm>(OnDeleteList);
            AddProductToListCommand = new Command<ShoppingListVm>(OnAddProductToList);
            CloseListCommand = new Command<ShoppingListVm>(OnCloseList);
            EditListCommand = new Command<ShoppingListVm>(async(shoppingList) => await OnEditList(shoppingList));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            await Task.Yield();
        }

        public async Task GetAllShoppingListsAndInitView()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();

            ActiveShoppingLists = new ObservableCollection<ShoppingListVm>(shoppingLists.Select(x=>new ShoppingListVm(x)));

            var closedList = MockedData.GetShoppingList(5, "Imprezka na weekend - nieaktywana już", 1, ShoppingListStatus.Closed);

            ClosedShoppingLists = new ObservableCollection<ShoppingListVm> { new ShoppingListVm(closedList) };

            RaisePropertyChanged(nameof(ActiveShoppingLists));
            RaisePropertyChanged(nameof(ClosedShoppingLists));
        }

        private async Task OnEditList(ShoppingListVm shoppingList)
        {
            await NavigateToWithMessage<EditShoppingListView, EditShoppingListViewModel>(new BaseMessage<EditShoppingListViewModel>(MessagesKeys.ShoppingListKey, shoppingList));
        }

        private void OnCloseList(ShoppingListVm shoppingList)
        {
        }

        private void OnAddProductToList(ShoppingListVm shoppingList)
        {
        }

        private void OnDeleteList(ShoppingListVm shoppingList)
        {
            if (shoppingList.Status == ShoppingListStatus.Active)
            {
                ActiveShoppingLists.Remove(shoppingList);
            }
            else
            {
                ClosedShoppingLists.Remove(shoppingList);
            }
        }

        private void OnAddNewListCommand()
        {
	        _alertsAndNotificationsProvider.ShowAlertWithTextField("Wpisz swoją nazwę listy", "Nowa lista zakupów", Keyboard.Default, AlertsAndNotificationsProviderOnAlertConfirmed);
        }

	    private void AlertsAndNotificationsProviderOnAlertConfirmed(string listName)
	    {
		    ActiveShoppingLists.Add(new ShoppingListVm(
			    new ShoppingList
			    {
				    Id = ActiveShoppingLists.Any()? ActiveShoppingLists.Max(x=>x.Id) + 1 : 0,
				    Name = listName,
				    AddDate = DateTime.UtcNow
			    }));
		    _alertsAndNotificationsProvider.ShowSuccessToast();
	    }

	    protected override async Task OnFeedback(IFeedbackMessage feedbackMessage)
	    {
		    var products = feedbackMessage.GetFirstOrDefaultProductsRange();
		    
		    if (products == null || !products.Any())
		    {
			    return;
		    }
		    
		    if (feedbackMessage.OperationMode == OperationMode.New)
		    {
			    var shoppingListId = feedbackMessage.GetInt(MessagesKeys.ShoppingListIdKey);
			    AddProducts(products, shoppingListId);
		    }
		    else if (feedbackMessage.OperationMode == OperationMode.Edit)
		    {
			    EditProducts(products);
		    }
	    }

	    private void AddProducts(IList<ProductVm> products, int? shoppingListId)
	    {
		    if (!shoppingListId.HasValue)
		    {
			    return;
		    }

		    var shoppingList = ActiveShoppingLists.FirstOrDefault(x => x.Id == shoppingListId);
		    shoppingList.AddProducts(products);
	    }
	    
	    private void EditProducts(IList<ProductVm> products)
	    {
		    foreach (var list in ActiveShoppingLists)
		    {
			    list.UpdateProducts(products);
		    }
	    }
    }
}