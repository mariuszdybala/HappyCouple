using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Providers.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class EditShoppingListViewModel : BaseHappyViewModel
    {
	    private readonly IShoppingListService _shoppingListService;
	    private readonly IAlertsAndNotificationsProvider _alertsAndNotificationsProvider;

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

        public Command AddProductCommand { get; set; }
        public Command DeleteListCommand => new Command(OnDeleteList);
	    public Command CloseListCommand => new Command(async () => await OnCloseList());
	    public Command<ProductVm> ProductCheckedCommand { get; set; }
        public Command<ProductVm> DeleteProductCommand { get; set; }
        public Command<ProductVm> EditProductCommand => new Command<ProductVm>(async (product) => await OnEditProduct(product));
	    public Command<ProductVm> ChangeQuantityCommand  => new Command<ProductVm>(OnChangeQuantity);

        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService, IShoppingListService shoppingListService, IAlertsAndNotificationsProvider alertsAndNotificationsProvider) : base(simpleAuthService)
        {
	        _shoppingListService = shoppingListService;
	        _alertsAndNotificationsProvider = alertsAndNotificationsProvider;

	        RegisterCommand();
        }

        private void RegisterCommand()
        {
            RegisterNavigateToMessage(this);

            DeleteProductCommand = new Command<ProductVm>(async(product) => await OnDeleteProduct(product));
            AddProductCommand = new Command(async () => await OnAddProduct());
            ProductCheckedCommand = new Command<ProductVm>(async (product) => await OnProductChecked(product));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
	        await Task.Yield();
	        
	        ShoppingList = (ShoppingListVm)message.GetValue(MessagesKeys.ShoppingListKey);
	        if (ShoppingList == null)
	        {
		        return;
	        }
			
	        ReloadProductsGroups();
	        ShoppingList.ProductChanged += ShoppingListOnProductChanged;
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

	    private async Task OnCloseList()
	    {
		    if (ShoppingList.IsListCompleted)
		    {
			    await CloseListConfiramtion(true);
			    return;
		    }

		    _alertsAndNotificationsProvider.ShowAlertWithConfirmation("mimo, że nie wszystkie produkty zostały zakupione.", "Lista zostanie zakmknięta",
			    async (confirmed) => await CloseListConfiramtion(confirmed));
	    }

	    private async Task CloseListConfiramtion(bool confirmed)
	    {
		    if (confirmed)
		    {
			    await SendFeedbackMessage<ShoppingsViewModel>(async (shoppingsViewModel) => await shoppingsViewModel.CloseListConfiramtion(true, ShoppingList));
			    await NavigateBack();
		    }
	    }

	    private void ShoppingListOnProductChanged(OperationMode operationMode)
	    {
		    ReloadProductsGroups();
	    }

	    private async Task OnAddProduct()
        {
	        await NavigateToWithMessage<FavouriteProductTypesView, FavouriteProductTypeViewModel>(new BaseMessage<FavouriteProductTypeViewModel>(MessagesKeys.ShoppingListIdKey, _shoppingList.Id));
        }

        private async Task OnProductChecked(ProductVm product)
        {
	        await _shoppingListService.UpdateProductAsync(product);
	        ShoppingList.CalculateCurrentShoppingProgress();
	        await ShoppingListChanged(ShoppingList);
        }

        private async Task OnDeleteProduct(ProductVm product)
        {
	        DeleteProductFromView(product);
	        await _shoppingListService.DeleteProductAsync(product);
	        await ShoppingListChanged(ShoppingList);
        }
	    
	    private async Task ShoppingListChanged(ShoppingListVm shoppingList)
	    {
		    shoppingList.EditDate = DateTime.Now;
		    await _shoppingListService.UpdateShoppingListAsync(shoppingList);
	    }
	    
	    private async Task OnEditProduct(ProductVm product)
	    {
		    var message = new BaseMessage<AddProductViewModel>();
		    message.AddData(MessagesKeys.ProductTypeKey, product.ProductType);
		    message.AddData(MessagesKeys.ProductKey, product);
		    
		    await NavigateToWithMessage<AddProductView, AddProductViewModel>(message);
	    }

	    protected override async Task OnFeedback(IFeedbackMessage feedbackMessage)
	    {
		    await Task.Yield();
		    ReloadProductsGroups();
	    }

        private void OnChangeQuantity(ProductVm product)
        {
	        _alertsAndNotificationsProvider.ShowAlertWithTextField("ilość produktu", "Wpisz nową", Keyboard.Numeric,
		        async (quantity) => await UpdateQuantityProduct(quantity, product));
        }

	    private async Task UpdateQuantityProduct(string quantity, ProductVm productVm)
	    {
		    int newquantityValue;
		    if (int.TryParse(quantity, out newquantityValue))
		    {
			    productVm.Quantity = newquantityValue;
			    await ShoppingListChanged(ShoppingList);
			    _alertsAndNotificationsProvider.ShowSuccessToast("Ilość zmieniona");
			    return;
		    }

		    _alertsAndNotificationsProvider.ShowFailedToast("Ilość błędna");
	    }

	    private void ReloadProductsGroups()
	    {
		    var groupedData = ShoppingList.Products.OrderBy(x => x.ProductType.Type)
			    .GroupBy(x => x.ProductType, new ProductTypeEqualityComparer())
			    .Select(x => new GroupedProductList(x))
			    .ToList();

		    GroupedProducts = new ObservableCollection<GroupedProductList>(groupedData);
	    }

	    public void DeleteProductFromView(ProductVm product)
	    {
		    var productGroup = GroupedProducts.FirstOrDefault(x => x.ProductType.Id == product.ProductType.Id);

		    if (productGroup == null)
		    {
			    return;
		    }

		    productGroup.Remove(product);

		    if (!productGroup.Any())
		    {
			    GroupedProducts.Remove(productGroup);
		    }

		    ShoppingList.DeleteProduct(product);
	    }

        protected override void CleanResources()
        {
	        ShoppingList.ProductChanged -= ShoppingListOnProductChanged;

	        GroupedProducts = null;
	        ShoppingList = null;
        }
    }
}
