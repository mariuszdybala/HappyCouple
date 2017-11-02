﻿using System.Collections.Generic;
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
		private int? _shoppingListId;
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
			var productTypes = await _productService.GetAllProductTypesAync();

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
