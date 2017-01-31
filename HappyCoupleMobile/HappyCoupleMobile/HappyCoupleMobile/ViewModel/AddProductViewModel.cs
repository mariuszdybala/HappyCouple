using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.ViewModel.Abstract;

namespace HappyCoupleMobile.ViewModel
{
    public class AddProductViewModel : BaseHappyViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<ProductType> ProductTypes { get; set; }

        public AddProductViewModel(ISimpleAuthService simpleAuthService, IProductService productService) : base(simpleAuthService)
        {
            _productService = productService;

            ProductTypes = new ObservableCollection<ProductType>();
            RegisterCommandAndMessages();
        }

        private void RegisterCommandAndMessages()
        {
            MessengerInstance.Register<IBaseMessage<AddProductViewModel>>(this, async(message) => await OnNavigateTo(message));
        }

        private async Task OnNavigateTo(IBaseMessage<AddProductViewModel> message)
        {
            await LoadProductTypes();
        }

        private async Task LoadProductTypes()
        {
            if (ProductTypes.Any())
            {
                return;
            }

            var productTypes = await _productService.GetPrimaryProductTypes();

            ProductTypes = new ObservableCollection<ProductType>(productTypes);

            RaisePropertyChanged(nameof(ProductTypes));
        }

        protected override void CleanResources()
        {
           // ProductTypes = new ObservableCollection<ProductType>();
        }
    }
}