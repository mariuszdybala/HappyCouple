using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;

namespace HappyCoupleMobile.ViewModel
{
	public class FavouriteProductTypeViewModel : BaseHappyViewModel
	{
		private readonly IShoppingListService _shoppingListService;
		private int? _shoppingListId;
		public ObservableCollection<ProductType> ProductTypes { get; set; }
		
		public RelayCommand<ProductType> ProductTypeTappedCommand => new RelayCommand<ProductType>(async(productType) => await OnProductTypeTapped(productType));

		public FavouriteProductTypeViewModel(ISimpleAuthService simpleAuthService, IShoppingListService shoppingListService) : base(simpleAuthService)
		{
			_shoppingListService = shoppingListService;
			ProductTypes = new ObservableCollection<ProductType>();

			RegisterCommand();
		}

		protected override async Task OnNavigateTo(IMessageData message)
		{
			_shoppingListId = message.GetInt(MessagesKeys.ShoppingListIdKey);
			await LoadProductTypes();
		}
		
		private async Task OnProductTypeTapped(ProductType productType)
		{
			var message = new BaseMessage<FavouriteProductsViewModel>(MessagesKeys.ProductTypeKey, productType);
			message.AddData(MessagesKeys.ShoppingListIdKey, _shoppingListId);
			await NavigateToWithMessage<FavouriteProductsView, FavouriteProductsViewModel>(message);
		}
		
		private async Task LoadProductTypes()
		{
			var productTypes = await _shoppingListService.GetAllProductTypesAync();

			ProductTypes = new ObservableCollection<ProductType>(productTypes);
			RaisePropertyChanged(nameof(ProductTypes));
		}
		
		private void RegisterCommand()
		{
			_shoppingListId = null;
			RegisterNavigateToMessage(this);
		}
	}
}
