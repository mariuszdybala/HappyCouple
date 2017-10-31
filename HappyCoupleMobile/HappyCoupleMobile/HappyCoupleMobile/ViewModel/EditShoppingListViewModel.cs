using System.Collections.Generic;
using System.Threading.Tasks;
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

        public ShoppingListVm ShoppingList
        {
            get { return _shoppingList; }
            set { Set(ref _shoppingList, value); }
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
        }

	    private async Task OnAddProduct()
        {
	        await NavigateTo<FavouriteProductTypesView, FavouriteProductTypeViewModel>();
        }

        protected override async Task OnFeedback(IFeedbackMessage feedbackMessage)
        {
	        var newProductVm = feedbackMessage.GetFirstOrDefaultProductsRange();

            ShoppingList.AddProductsRange(newProductVm);
        }

        private async Task OnProductChecked(ProductVm product)
        {
            ShoppingList.CalculateCurrentShoppingProgress();

            await Task.Yield();
        }

        private void OnDeleteProduct(ProductVm product)
        {
            ShoppingList.DeleteProduct(product);
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

        protected override void CleanResources()
        {
        }
    }
}
