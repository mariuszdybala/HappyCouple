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
    public class EditShoppingListViewModel : BaseHappyViewModel
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
            RegisterNavigateToMessage(this);

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
            RegisterFeedBackMessage(this, true);
            await NavigateTo<AddProductView, AddProductViewModel>();

        }

        protected override async Task OnFeedback(IFeedbackMessage feedbackMessage)
        {
            var newProductVm = (ProductVm)feedbackMessage.GetValue(MessagesKeys.ProductKey);

            ShoppingList.Products.Add(newProductVm);
            ShoppingList.CalculateCurrentShoppingProgress();
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

        protected override void CleanResources()
        {
            UnSubscribeAllEventsFromViewCommand.Execute(null);
        }
    }
}