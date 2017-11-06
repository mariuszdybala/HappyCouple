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
	    private readonly IShoppingListService _shoppingListService;
	    private ProductType _productType;
	    private ProductVm _originProduct;
	    private ProductVm _product;
	    private string _saveButtonText;
	    private bool _editing;

	    public string SaveButtonText
	    {
		    get => _saveButtonText;
		    set => Set(ref _saveButtonText, value);
	    }
	    
	    public ProductVm Product
	    {
		    get => _product;
		    set => Set(ref _product, value);
	    }
	    
	    public ProductVm OriginProduct
	    {
		    get => _originProduct;
		    set => Set(ref _originProduct, value);
	    }

        public ProductType ProductType
        {
            get => _productType;
	        set => Set(ref _productType, value);
        }

	    public bool Editing
	    {
		    get => _editing;
		    set
		    {
			    _editing = value;
			    SaveButtonText = _editing ? "Update product" : "Add product";
		    }
	    }

        public ICommand SaveProductCommand { get; set; }

        public AddProductViewModel(ISimpleAuthService simpleAuthService, IShoppingListService shoppingListService) : base(simpleAuthService)
        {
	        _shoppingListService = shoppingListService;
	        RegisterCommandAndMessages();
        }

        private void RegisterCommandAndMessages()
        {
            RegisterNavigateToMessage(this);

            SaveProductCommand = new Command(async () => await OnSaveProduct());
        }

        protected override async Task OnNavigateTo(IMessageData message)
        {
	        await Task.Yield();
	        
	        ProductType = (ProductType)message.GetValue(MessagesKeys.ProductTypeKey);

	        var product = (ProductVm) message.GetValue(MessagesKeys.ProductKey);
	        
	        Product =  product ?? ProductVm.CreateProductVm(null, null, 0, ProductType, Admin);

	        Editing = product != null;
	        SaveOriginProductInEditingMode();
        }

	    private void SaveOriginProductInEditingMode()
	    {
		    if (!Editing)
		    {
			    return;
		    }
		    
		    OriginProduct = ProductVm.CreateProductVm(Product.Name, Product.Comment);
	    }

	    private async Task OnSaveProduct()
        {
	        if (Product.IsFavourite)
	        {
		        await _shoppingListService.UpdateFavouriteProductAsync(Product);
	        }
	        else
	        {
		        await _shoppingListService.UpdateProductAsync(Product);
	        }
	        
			await SendFeedbackMessage(new FeedbackMessage(MessagesKeys.ProductKey, Product, Editing ? OperationMode.Update : OperationMode.InsertNew));

            await NavigateBack();
        }

	    protected override Task OnGoBackCommand()
	    {
		    if (!Editing)
		    {
			    return base.OnGoBackCommand();
		    }
		    
		    Product.Comment = OriginProduct.Comment;
		    Product.Name = OriginProduct.Name;

		    return base.OnGoBackCommand();
	    }

	    protected override void CleanResources()
        {
            ProductType = null;
	        Product = null;
	        OriginProduct = null;
	        Editing = false;
        }
    }
}