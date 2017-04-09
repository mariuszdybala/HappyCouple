using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Notification.Interfaces;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class ShoppingsViewModel : BaseHappyViewModel, IProductObserver, IShoppingListObserver
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly INotificationManager _notificationManager;

        private bool _showAddNewListPopUp;

        public bool ShowAddNewListPopUp
        {
            get { return _showAddNewListPopUp; }
            set { Set(ref _showAddNewListPopUp, value); }
        }

        public Command<string> AddNewListCommand { get; set; }
        public Command<ShoppingList> DeleteListCommand { get; set; }
        public Command<ShoppingList> AddProductToListCommand { get; set; }
        public Command<ShoppingList> CloseListCommand { get; set; }
        public Command<ShoppingList> EditListCommand { get; set; }
        public Command CloseAddNewListPopUpCommand { get; set; }


        public ObservableCollection<ShoppingList> ActiveShoppingLists { get; set; }
        public ObservableCollection<ShoppingList> ClosedShoppingLists { get; set; }

        public ShoppingsViewModel(ISimpleAuthService simpleAuthService, IShoppingListRepository shoppingListRepository, INotificationManager notificationManager) : base(simpleAuthService)
        {
            _shoppingListRepository = shoppingListRepository;
            _notificationManager = notificationManager;
            RegisterCommand();

            ActiveShoppingLists = new ObservableCollection<ShoppingList>();
        }

        private void RegisterCommand()
        {
            AddNewListCommand = new Command<string>(OnAddNewListCommand);
            DeleteListCommand = new Command<ShoppingList>(OnDeleteList);
            AddProductToListCommand = new Command<ShoppingList>(OnAddProductToList);
            CloseListCommand = new Command<ShoppingList>(OnCloseList);
            EditListCommand = new Command<ShoppingList>(async(shoppingList) => await OnEditList(shoppingList));
            CloseAddNewListPopUpCommand = new Command(OnCloseAddNewListPopUpCommand);
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            await Task.Yield();
        }

        public async Task GetAllShoppingListsAndInitView()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListWithProductsAsync();

            ActiveShoppingLists = new ObservableCollection<ShoppingList>(shoppingLists);

            ClosedShoppingLists = new ObservableCollection<ShoppingList> {ActiveShoppingLists.Last()};

            RaisePropertyChanged(nameof(ActiveShoppingLists));
            RaisePropertyChanged(nameof(ClosedShoppingLists));
        }



        private async Task OnEditList(ShoppingList shoppingList)
        {
            await NavigateToWithMessage<EditShoppingListView, EditShoppingListViewModel>(new BaseMessage<EditShoppingListViewModel>("ShoppingList", shoppingList));
        }

        private void OnCloseList(ShoppingList shoppingList)
        {
            _notificationManager.UpdateProduct(new Product());
        }

        private void OnAddProductToList(ShoppingList shoppingList)
        {
        }

        private void OnDeleteList(ShoppingList shoppingList)
        {
        }

        private void OnCloseAddNewListPopUpCommand()
        {
            ShowAddNewListPopUp = false;
        }

        private void OnAddNewListCommand(string newListName)
        {
            ShowAddNewListPopUp = true;
        }

        public void Upadte(Product data)
        {
        }

        public void Remove(Product data)
        {
        }

        public void Add(Product data)
        {
        }

        public void Upadte(ShoppingList data)
        {
        }

        public void Remove(ShoppingList data)
        {
        }

        public void Add(ShoppingList data)
        {
        }
    }
}