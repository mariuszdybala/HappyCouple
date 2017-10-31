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
		private readonly IProductServices _productService;
		public ObservableCollection<ProductType> ProductTypes { get; set; }
		
		public RelayCommand<ProductType> ProductTypeTappedCommand => new RelayCommand<ProductType>(async(productType) => await OnProductTypeTapped(productType));

		public FavouriteProductTypeViewModel(ISimpleAuthService simpleAuthService, IProductServices productService) : base(simpleAuthService)
		{
			_productService = productService;
			ProductTypes = new ObservableCollection<ProductType>();

			RegisterCommand();
		}

		protected override async Task OnNavigateTo(IMessageData message)
		{
			await LoadProductTypes();
		}
		
		private async Task OnProductTypeTapped(ProductType productType)
		{
			await NavigateToWithMessage<FavouriteProductsView, FavouriteProductsViewModel>(new BaseMessage<FavouriteProductsViewModel>(MessagesKeys.ProductTypeKey, productType));
			
		}
		
		private async Task LoadProductTypes()
		{
			var productTypes = await _productService.GetAllProductTypesAync();

			ProductTypes = new ObservableCollection<ProductType>(productTypes);
			RaisePropertyChanged(nameof(ProductTypes));
		}
		
		private void RegisterCommand()
		{
			RegisterNavigateToMessage(this);
		}
	}
}
