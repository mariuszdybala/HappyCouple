using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.ViewModel.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Providers.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class FavouriteProductsViewModel : BaseHappyViewModel
    {
        private readonly IProductServices _productService;
	    private readonly IAlertsAndNotificationsProvider _alertsAndNotificationsProvider;
	    private ProductType _selectedProductType;

        public ProductType SelectedProductType
        {
            get { return _selectedProductType; }
            set { Set(ref _selectedProductType, value); }
        }
	    
	    private ObservableCollection<ProductVm>  _mockListForProduct;

	    public ObservableCollection<ProductVm>  MockListForProduct
	    {
		    get { return _mockListForProduct; }
		    set { Set(ref _mockListForProduct, value); }
	    }


        public RelayCommand<ProductVm> DeleteProductCommand => new RelayCommand<ProductVm>(OnDeleteProduct);
        public RelayCommand<ProductVm> ProductSelectedCommand => new RelayCommand<ProductVm>(async(product) => await OnProductSelected(product));
	    public RelayCommand<ProductVm> EditProductCommand => new RelayCommand<ProductVm>(async (product) => await OnEditProduct(product));
	    public RelayCommand<ProductVm> GoToAddProductToFavoriteCommand => new RelayCommand<ProductVm>(async async  => await OnGoToAddProductToFavorite());

        public RelayCommand<ProductType> ProductTypeSelectedCommand { get; set; }

        public FavouriteProductsViewModel(ISimpleAuthService simpleAuthService, IProductServices productService, IAlertsAndNotificationsProvider alertsAndNotificationsProvider) : base(simpleAuthService)
        {
            _productService = productService;
	        _alertsAndNotificationsProvider = alertsAndNotificationsProvider;
            MockListForProduct = new ObservableCollection<ProductVm>();
            RegisterCommandAndMessages();
        }

        public void RegisterCommandAndMessages()
        {
            RegisterNavigateToMessage(this);
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
	        SelectedProductType = (ProductType)message.GetValue(MessagesKeys.ProductTypeKey);
	        
            LoadProductTypes();

	        await Task.Yield();
        }

	    private async Task OnGoToAddProductToFavorite()
	    {
		    await NavigateToWithMessage<AddProductView, AddProductViewModel>(new BaseMessage<AddProductViewModel>(MessagesKeys.ProductTypeKey, SelectedProductType));
	    }

	    protected override async Task OnFeedback(IFeedbackMessage feedbackMessage)
	    {
		    if (feedbackMessage.OperationMode != OperationMode.New)
		    {
			    MockListForProduct = new ObservableCollection<ProductVm>(MockListForProduct);
			    return;
		    }
		    
		    var product = feedbackMessage.GetFirstOrDefaultProduct();

		    if (product == null)
		    {
			    return;
		    }
			    
		    MockListForProduct.Insert(0,product);
		    
		    await Task.Yield();
	    }

	    private void OnDeleteProduct(ProductVm product)
        {
	        if (MockListForProduct.Remove(product))
	        {
		        _alertsAndNotificationsProvider.ShowSuccessToast("Usunięto");
	        }
	        else
	        {
		        _alertsAndNotificationsProvider.ShowFailedToast();
	        }
        }

	    private async Task OnEditProduct(ProductVm product)
	    {
		    var message = new BaseMessage<AddProductViewModel>();
		    message.AddData(MessagesKeys.ProductTypeKey, SelectedProductType);
		    message.AddData(MessagesKeys.ProductKey, product);

		    await NavigateToWithMessage<AddProductView, AddProductViewModel>(message);
	    }

        private async Task OnProductTypeSelected(ProductType productType)
        {
            await Task.Yield();
        }

        private async Task OnProductSelected(ProductVm product)
        {
	        _alertsAndNotificationsProvider.ShowAlertWithTextField("ilość produktu", "Wpisz", Keyboard.Numeric);
	        _alertsAndNotificationsProvider.AlertConfirmed += AlertsAndNotificationsProviderOnAlertConfirmed;
            await Task.Yield();
        }

	    private void AlertsAndNotificationsProviderOnAlertConfirmed(string productQuantity)
	    {
		    int quantity;

		    if (int.TryParse(productQuantity, out quantity))
		    {
			    _alertsAndNotificationsProvider.ShowSuccessToast("Produkt dodany");
			    return;
		    }
		    _alertsAndNotificationsProvider.ShowFailedToast("Ilość błędna");
	    }

	    private void LoadProductTypes()
        {
	        
	        
            var mockListForProduct =
                new List<Product>
                {
                    new Product
                    {
                        Id = 0,
                        Name = "Marcheweczka",
                        Comment = "To jest pyszna marcheweczka trzeba ją kupić",
                        Quantity = 4
                    },
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 1, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
	                new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 4, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 5, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 6, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 6, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 6, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 6, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 6, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 6, Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
	                new Product {Id = 7, Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10}
	            };

            MockListForProduct = new ObservableCollection<ProductVm>(mockListForProduct.Select(x=> new ProductVm(x)));

            RaisePropertyChanged(nameof(MockListForProduct));
        }

        protected override void CleanResources()
        {

        }
    }
}
