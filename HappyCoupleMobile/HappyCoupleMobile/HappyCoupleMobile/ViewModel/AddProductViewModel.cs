using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel.Abstract;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel
{
    public class AddProductViewModel : BaseHappyViewModel
    {
        private readonly IProductServices _productService;
        private string _productName;
        private string _productQuantity;
        private string _productComment;
        private ProductType _productType;

        public string ProductName
        {
            get { return _productName; }
            set { Set(ref _productName, value); }
        }

        public string ProductQuantity
        {
            get { return _productQuantity; }
            set { Set(ref _productQuantity, value); }
        }

        public string ProductComment
        {
            get { return _productComment; }
            set { Set(ref _productComment, value); }
        }

        public ProductType ProductType
        {
            get { return _productType; }
            set { Set(ref _productType, value); }
        }

        public ICommand GoToFavouriteProductsCommand { get; set; }
        public ICommand SaveProductCommand { get; set; }

        public AddProductViewModel(ISimpleAuthService simpleAuthService, IProductServices productService) : base(simpleAuthService)
        {
            _productService = productService;
            RegisterCommandAndMessages();
        }

        private void RegisterCommandAndMessages()
        {
            RegisterNavigateToMessage(this);

            GoToFavouriteProductsCommand = new Command(async () => await OnGoToFavouriteProducts());
            SaveProductCommand = new Command(async () => await OnSaveProduct());
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
	        ProductType = (ProductType)message.GetValue(MessagesKeys.ProductTypeKey);
        }

        private async Task OnGoToFavouriteProducts()
        {
            await NavigateTo<FavouriteProductsView, FavouriteProductsViewModel>();
        }

        private async Task OnSaveProduct()
        {
            if (ProductType == null)
            {
                ShowAlertMessage(AlertType.Information, Messages.ProductTypeIsMandatory);
                return;
            }

            var newProduct = _productService.CreateProductVm(ProductName, ProductComment, ProductQuantity, ProductType, Admin);

            SendFeedbackMessage(new FeedbackMessage(MessagesKeys.ProductKey, newProduct));

            await NavigateBack();
        }

        private async Task OnNavigateTo(IBaseMessage<AddProductViewModel> message)
        {
        }

        protected override void CleanResources()
        {
            ProductType = null;
            ProductComment = ProductQuantity = ProductName = null;
        }
    }
}