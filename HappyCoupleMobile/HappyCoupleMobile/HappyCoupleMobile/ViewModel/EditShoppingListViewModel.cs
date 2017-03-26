using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class EditShoppingListViewModel : BaseHappyViewModel, IProductObserver, IShoppingListObserver
    {
        private readonly IProductServices _productServices;
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand<Product> ProductCheckedCommand { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public Command GoBackCommand { get; set; }

        public Command AddProductButtonCommand { get; set; }


        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService, IProductServices productServices) : base(simpleAuthService)
        {
            _productServices = productServices;
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            RegisterMessage(this);

            //mocks
            AddProductButtonCommand = new Command(AddProductButton);

            AddProductCommand = new RelayCommand(async () => await OnAddProduct());
            ProductCheckedCommand = new RelayCommand<Product>(async (product) => await OnProductChecked(product));
        }

        private void AddProductButton()
        {
            GoBackCommand.Execute(new Product{Name = "Test"});
        }


        protected override async Task OnNavigateTo(IMessageData message)
        {
            var types = await _productServices.GetAllProductTypesAync();


            Products = new ObservableCollection<Product>(
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

            RaisePropertyChanged(nameof(Products));
        }

        private async Task OnAddProduct()
        {
            await NavigateTo<AddProductView, AddProductViewModel>();
        }
        private Task OnProductChecked(Product product)
        {
            return null;
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