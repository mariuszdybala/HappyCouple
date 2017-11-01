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
        private readonly IProductServices _productServices;
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

        public Command<ProductVm> ProductCheckedCommand { get; set; }
        public Command<ProductVm> DeleteProductCommand { get; set; }
        public Command<ProductVm> EditProductCommand { get; set; }

        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService, IProductServices productServices, IAlertsAndNotificationsProvider alertsAndNotificationsProvider) : base(simpleAuthService)
        {
            _productServices = productServices;
	        _alertsAndNotificationsProvider = alertsAndNotificationsProvider;

	        RegisterCommand();        
        }

        private void RegisterCommand()
        {
            RegisterNavigateToMessage(this);

            DeleteProductCommand = new Command<ProductVm>(OnDeleteProduct);
            EditProductCommand = new Command<ProductVm>(async(product) => await OnEditProduct(product));
            AddProductCommand = new Command(async () => await OnAddProduct());
            ProductCheckedCommand = new Command<ProductVm>(async (product) => await OnProductChecked(product));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
	        ShoppingList = (ShoppingListVm)message.GetValue(MessagesKeys.ShoppingListKey);
	        if (ShoppingList == null)
	        {
		        return;
	        }
	        
	        ReloadProductsGroups();
	        ShoppingList.ProductChanged += ShoppingListOnProductChanged;
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
	        ShoppingList.CalculateCurrentShoppingProgress();

            await Task.Yield();
        }

        private void OnDeleteProduct(ProductVm product)
        {
            DeleteProduct(product);
        }

        private async Task OnEditProduct(ProductVm product)
        {
	        _alertsAndNotificationsProvider.ShowAlertWithTextField("ilość produktu", "Wpisz nową", Keyboard.Numeric,
		        (quantity) =>
		        {
			        int newquantityValue;
			        if (int.TryParse(quantity, out newquantityValue))
			        {
				        product.Quantity = newquantityValue;
				        _alertsAndNotificationsProvider.ShowSuccessToast("Ilość zmieniona");
				        return;
			        }

			        _alertsAndNotificationsProvider.ShowFailedToast("Ilość błędna");
		        });
	        
	        await Task.Yield(); 
        }
	    
	    private void ReloadProductsGroups()
	    {
		    var groupedData = ShoppingList.Products.OrderBy(x => x.ProductType.Type)
			    .GroupBy(x => x.ProductType, new ProductTypeEqualityComparer())
			    .Select(x => new GroupedProductList(x))
			    .ToList();

		    GroupedProducts = new ObservableCollection<GroupedProductList>(groupedData);
	    }
	    
	    public void DeleteProduct(ProductVm product)
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

	        ShoppingList = null;
        }
    }
}
