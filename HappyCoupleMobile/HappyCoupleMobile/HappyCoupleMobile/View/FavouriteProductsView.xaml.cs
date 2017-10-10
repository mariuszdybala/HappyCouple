using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.View.Abstract;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.View
{
    public partial class FavouriteProductsView : BaseHappyContentPage
    {
	    private readonly string _okButtonText = "OK";
	    private readonly string _cancelButtonText = "Cancel";
	    
	    public static BindableProperty TapProductCommandProperty =
		    BindableProperty.Create(nameof(TapProductCommand), typeof(ICommand), typeof(FavouriteProductsView));

	    public ICommand TapProductCommand
	    {
		    get => (ICommand) GetValue(TapProductCommandProperty);
		    set => SetValue(TapProductCommandProperty, value);
	    }
	    
	    public RelayCommand<ProductVm> TapProductInternalCommand => new RelayCommand<ProductVm>(OnTapProductInternal);

	    public FavouriteProductsView()
        {
            InitializeComponent();

	        OkPanelButton.ButtonText = _okButtonText;
	        CancelPanelButton.ButtonText = _cancelButtonText;

	        OkPanelButton.Clicked += OnPanelButtonClicked;
	        CancelPanelButton.Clicked += OnPanelButtonClicked;
        }
	    
	    private void OnTapProductInternal(ProductVm product)
	    {
		    QuantityPanel.IsVisible = true;

		    if (TapProductCommand != null && TapProductCommand.CanExecute(product))
		    {
			    TapProductCommand.Execute(product);
		    }
	    }

	    private void OnPanelButtonClicked(string buttonActionText)
	    {
		    if (buttonActionText == _cancelButtonText)
		    {
			    QuantityPanel.IsVisible = false;
		    }
	    }
    }
}
