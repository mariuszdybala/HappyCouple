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
        private string _boughtProductsCount;
        private string _leftProductsCount;
        private string _progressPercent;
        private string _shoppingListName;

        public string BoughtProductsCount
        {
            get { return _boughtProductsCount; }
            set { Set(ref _boughtProductsCount, value); }
        }

        public string LeftProductsCount
        {
            get { return _leftProductsCount; }
            set { Set(ref _leftProductsCount, value); }
        }

        public string ProgressPercent
        {
            get { return _progressPercent; }
            set { Set(ref _progressPercent, value); }
        }

        public string ShoppingListName
        {
            get { return _shoppingListName; }
            set { Set(ref _shoppingListName, value); }
        }

        public Command AddProductCommand { get; set; }

        public Command<ProductVm> ProductCheckedCommand { get; set; }
        public Command<ProductVm> DeleteProductCommand { get; set; }
        public Command<ProductVm> EditProductCommand { get; set; }
        public Command UnSubscribeAllEventsFromViewCommand { get; set; }

        public ObservableCollection<ProductVm> Products { get; set; }


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
            var shoppingList = message.GetValue<ShoppingList>();


            Products = new ObservableCollection<ProductVm>(shoppingList.Products.Select(x => new ProductVm(x)));

            SetViewProperties(shoppingList.Name);

            RaisePropertyChanged(nameof(Products));
        }

        private void SetViewProperties(string shoppingListName)
        {
            ShoppingListName = shoppingListName;
            CalculateCurrentShoppingProgress();
        }

        private void CalculateCurrentShoppingProgress()
        {
            var totalProductsCountValue = Products.Count;
            var boughtProductsCountValue = Products.Count(x => x.ProductModel.IsBought);
            var leftProductsCountValue = Products.Count(x => !x.ProductModel.IsBought);

            BoughtProductsCount = boughtProductsCountValue.ToString();
            LeftProductsCount = leftProductsCountValue.ToString();

            var progressPercent = leftProductsCountValue == 0 ? 100 : boughtProductsCountValue * 100 / totalProductsCountValue;

            ProgressPercent = progressPercent.ToString();
        }

        private async Task OnAddProduct()
        {
            await NavigateTo<AddProductView, AddProductViewModel>();
        }
        private async Task OnProductChecked(ProductVm product)
        {
            CalculateCurrentShoppingProgress();

            Task.Yield();
        }

        private void OnDeleteProduct(ProductVm product)
        {
            Products.Remove(product);
            CalculateCurrentShoppingProgress();
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