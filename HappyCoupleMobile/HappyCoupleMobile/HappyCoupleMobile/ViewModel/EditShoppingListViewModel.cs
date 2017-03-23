using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
    public class EditShoppingListViewModel : BaseHappyViewModel, IProductObserver
    {
        private readonly IProductServices _propProductServices;
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand ClickCommand { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public EditShoppingListViewModel(ISimpleAuthService simpleAuthService, IProductServices propProductServices) : base(simpleAuthService)
        {
            _propProductServices = propProductServices;
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            RegisterMessage(this);

            AddProductCommand = new RelayCommand(async () => await OnAddProduct());
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
            var shoppigList = message.GetValue<ShoppingList>();

            LoadDataOnView(shoppigList);
        }

        private void LoadDataOnView(ShoppingList shoppigList)
        {
            var products = shoppigList.Products;

            Products = new ObservableCollection<Product>(products);

            RaisePropertyChanged(nameof(Products));
        }

        private async Task OnAddProduct()
        {
            await NavigateTo<AddProductView, AddProductViewModel>();
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

        public void Remove<TData>(TData data)
        {
            throw new System.NotImplementedException();
        }

        public void Add<TData>(TData data)
        {
            throw new System.NotImplementedException();
        }
    }
}