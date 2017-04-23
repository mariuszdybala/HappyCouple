using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Notification.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class EditShoppingListViewModel : BaseHappyViewModel, IProductObserver, IShoppingListObserver
    {
        private readonly IProductServices _productServices;
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
        public Command UnSubscribeAllEventsFromViewCommand { get; set; }

        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService, IProductServices productServices) : base(simpleAuthService)
        {
            _productServices = productServices;

            RegisterCommand();
        }

        private void RegisterCommand()
        {
            RegisterMessage(this);

            DeleteProductCommand = new Command<ProductVm>(OnDeleteProduct);
            EditProductCommand = new Command<ProductVm>(OnEditProduct);
            AddProductCommand = new Command(async () => await OnAddProduct());
            ProductCheckedCommand = new Command<ProductVm>(async (product) => await OnProductChecked(product));
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            ShoppingList = (ShoppingListVm)message.GetValue(MessagesKeys.ShoppingListKey);
        }

        private async Task OnAddProduct()
        {
            await NavigateTo<AddProductView, AddProductViewModel>();
        }
        private async Task OnProductChecked(ProductVm product)
        {
            ShoppingList.CalculateCurrentShoppingProgress();

            Task.Yield();
        }

        private void OnDeleteProduct(ProductVm product)
        {
            ShoppingList.Products.Remove(product);
            ShoppingList.CalculateCurrentShoppingProgress();
        }

        private void OnEditProduct(ProductVm product)
        {
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


        protected override void CleanResources()
        {
            UnSubscribeAllEventsFromViewCommand.Execute(null);
        }
    }
}