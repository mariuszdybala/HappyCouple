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
	    public static BindableProperty TapProductCommandProperty =
		    BindableProperty.Create(nameof(TapProductCommand), typeof(ICommand), typeof(FavouriteProductsView));

	    public ICommand TapProductCommand
	    {
		    get => (ICommand) GetValue(TapProductCommandProperty);
		    set => SetValue(TapProductCommandProperty, value);
	    }

	    public FavouriteProductsView()
        {
            InitializeComponent();
        }
    }
}
