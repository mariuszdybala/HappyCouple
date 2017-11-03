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
		private IList<ProductVm> _addedProducts;
		private IList<ProductVm> _editedProducts;

		private readonly IProductServices _productService;
		private readonly IAlertsAndNotificationsProvider _alertsAndNotificationsProvider;
		private ProductType _selectedProductType;
		private ObservableCollection<ProductVm> _favouriteProducts;
		private int? _shoppingListId;
		private bool _emptyListPlaceholder;

		public bool EmptyListPlaceholder
		{
			get => _emptyListPlaceholder;
			set => Set(ref _emptyListPlaceholder, value);
		}

		public ProductType SelectedProductType
		{
			get => _selectedProductType;
			set => Set(ref _selectedProductType, value);
		}

		public ObservableCollection<ProductVm> FavouriteProducts
		{
			get => _favouriteProducts;
			set => Set(ref _favouriteProducts, value);
		}

		public RelayCommand<ProductVm> DeleteProductCommand => new RelayCommand<ProductVm>(OnDeleteProduct);

		public RelayCommand<ProductVm> ProductSelectedCommand =>
			new RelayCommand<ProductVm>(async (product) => await OnProductSelected(product));

		public RelayCommand<ProductVm> EditProductCommand =>
			new RelayCommand<ProductVm>(async (product) => await OnEditProduct(product));

		public RelayCommand<ProductVm> GoToAddProductToFavoriteCommand =>
			new RelayCommand<ProductVm>(async async => await OnGoToAddProductToFavorite());

		public RelayCommand<ProductType> ProductTypeSelectedCommand { get; set; }

		public FavouriteProductsViewModel(ISimpleAuthService simpleAuthService, IProductServices productService,
			IAlertsAndNotificationsProvider alertsAndNotificationsProvider) : base(simpleAuthService)
		{
			_productService = productService;
			_alertsAndNotificationsProvider = alertsAndNotificationsProvider;
			FavouriteProducts = new ObservableCollection<ProductVm>();
			RegisterCommandAndMessages();

			_addedProducts = new List<ProductVm>();
			_editedProducts = new List<ProductVm>();
		}

		public void RegisterCommandAndMessages()
		{
			RegisterNavigateToMessage(this);
		}

		protected override async Task OnNavigateTo(IMessageData message)
		{
			_shoppingListId = message.GetInt(MessagesKeys.ShoppingListIdKey);
			SelectedProductType = (ProductType) message.GetValue(MessagesKeys.ProductTypeKey);

			LoadFavouriteProducts();

			await Task.Yield();
		}

		private void LoadFavouriteProducts()
		{
			AddEmptyListPlaceholderIfNeeded();
			RaisePropertyChanged(nameof(FavouriteProducts));
		}

		private void AddEmptyListPlaceholderIfNeeded()
		{
			EmptyListPlaceholder = !FavouriteProducts.Any();
		}

		private async Task OnGoToAddProductToFavorite()
		{
			await NavigateToWithMessage<AddProductView, AddProductViewModel>(
				new BaseMessage<AddProductViewModel>(MessagesKeys.ProductTypeKey, SelectedProductType));
		}

		protected override async Task OnFeedback(IFeedbackMessage feedbackMessage)
		{
			var product = feedbackMessage.GetFirstOrDefaultProduct();

			if (product == null)
			{
				return;
			}

			if (feedbackMessage.OperationMode != OperationMode.New)
			{
				_editedProducts.Add(product);
				return;
			}

			FavouriteProducts.Insert(0, product);
			AddEmptyListPlaceholderIfNeeded();

			await Task.Yield();
		}

		private void OnDeleteProduct(ProductVm product)
		{
			if (FavouriteProducts.Remove(product))
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
			if (!_shoppingListId.HasValue)
			{
				return;
			}

			_alertsAndNotificationsProvider.ShowAlertWithTextField("ilość produktu", "Wpisz", Keyboard.Numeric, async (quantity) => await OnProductAdded(product, quantity));
		}

		private async Task OnProductAdded(ProductVm product, string quantity)
		{
			int quantityValue;

			if (int.TryParse(quantity, out quantityValue))
			{
				AddToProductBuffor(product, quantityValue);
				_alertsAndNotificationsProvider.ShowSuccessToast("Produkt dodany");
				return;
			}
			_alertsAndNotificationsProvider.ShowFailedToast("Ilość błędna");
		}

		private void AddToProductBuffor(ProductVm product, int quantity)
		{
			var newProduct = _productService.CreateProductVmFromFavouriteProduct(product, SelectedProductType, quantity, Admin);
			_addedProducts.Add(newProduct);
		}

		private async Task SendFeedbackAboutAddedProduct()
		{
			if (!_addedProducts.Any() || !_shoppingListId.HasValue)
			{
				return;
			}

			var feedBackMessage = new FeedbackMessage(MessagesKeys.ProductsKey, _addedProducts);
			feedBackMessage.OperationMode = OperationMode.New;
			feedBackMessage.AddData(MessagesKeys.ShoppingListIdKey, _shoppingListId);
			await SendFeedbackMessage<ShoppingsViewModel>(feedBackMessage);
		}

		private async Task SendFeedbackAboutChangedProduct()
		{
			if (!_editedProducts.Any())
			{
				return;
			}

			var feedBackMessage = new FeedbackMessage(MessagesKeys.ProductsKey, _editedProducts);
			feedBackMessage.OperationMode = OperationMode.Edit;
			await SendFeedbackMessage<ShoppingsViewModel>(feedBackMessage);
		}

		protected override async Task OnGoBackCommand()
		{
			await SendFeedbackAboutAddedProduct();
			await SendFeedbackAboutChangedProduct();
		    await base.OnGoBackCommand();
		}

		protected override void CleanResources()
		{
			_shoppingListId = null;
			_addedProducts.Clear();
			_editedProducts.Clear();

			EmptyListPlaceholder = false;
			SelectedProductType = null;
			FavouriteProducts.Clear();
		}
	}
}
