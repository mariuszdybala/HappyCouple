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

        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand<ProductVm> ProductCheckedCommand { get; set; }
        public Command<ProductVm> DeleteProductCommand { get; set; }

        public ObservableCollection<ProductVm> Products { get; set; }


        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService, IProductServices productServices) : base(simpleAuthService)
        {
            _productServices = productServices;
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            RegisterMessage(this);

            //mocks
            DeleteProductCommand = new Command<ProductVm>(OnDeleteProduct);


            AddProductCommand = new RelayCommand(async () => await OnAddProduct());
            ProductCheckedCommand = new RelayCommand<ProductVm>(async (product) => await OnProductChecked(product));
        }

        private async void AddProductButton()
        {
            var types = await _productServices.GetAllProductTypesAync();

            var newProduct = new Product
            {
                Id = 5,
                ProductType = types[6],
                Name = "Marcheweczka",
                Comment = "To jest pyszna marcheweczka trzeba ją kupić",
                Quantity = 4
            };

            Products.Add(new ProductVm(newProduct));

            RaisePropertyChanged(nameof(Products));
        }


        protected override async Task OnNavigateTo(IMessageData message)
        {
            var types = await _productServices.GetAllProductTypesAync();

            var productsModels = new List<Product>(
                new List<Product>
                {
                    new Product
                    {
                        Id = 0,
                        ProductType =  types[2],
                        Name = "Marcheweczka",
                        Comment = "To jest pyszna marcheweczka trzeba ją kupić",
                        Quantity = 4
                    },
                    new Product {Id = 1,ProductType =  types[0], Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10},
                    new Product {Id = 2,ProductType =  types[1],Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 3,ProductType =  types[1], Name = "Tuńczyk", Comment = "Steki w Biedronce", Quantity = 1},
                    new Product {Id = 4,ProductType =  types[0], Name = "Piweczko", Comment = "MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) MMM pyszne piweczko :) ", Quantity = 10}
                });

            Products = new ObservableCollection<ProductVm>(productsModels.Select(x => new ProductVm(x)));

            SetViewProperties();

            RaisePropertyChanged(nameof(Products));
        }

        private void SetViewProperties()
        {
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
    }
}