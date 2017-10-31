using System;
using System.ComponentModel;
using Foundation;
using HappyCoupleMobile.iOS.Delegates;
using UIKit;
using SWTableViewCells;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HappyCoupleMobile.VM;

namespace HappyCoupleMobile.iOS.Controls
{
    public partial class ExtendedSwipeAbleCell : SWTableViewCell
    {
        public static readonly NSString Key = new NSString("ExtendedSwipeAbleCell");
        public static readonly UINib Nib;

        private ProductVm _product;
        public ProductVm Product
        {
            get => _product;
	        set
	        {
		        _product = value;
		        UpdateProduct();
	        }
        }

        static ExtendedSwipeAbleCell()
        {
            Nib = UINib.FromName("ExtendedSwipeAbleCell", NSBundle.MainBundle);
        }

        protected ExtendedSwipeAbleCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

	    private void UpdateProduct()
	    {
		    Name.Text = Product.Name;
		    Comment.Text = Product.Comment;
		    Quantity.Text = Product.Quantity.ToString();
		    ToggleBoughtCheckbox(Product.IsBought);
	    }

        public void UpdateCell(ProductVm product, Action productSelected, bool hideCheckbox = false, bool hideProductQuantity = false)
        {
            Product = product;
	        Product.PropertyChanged -= OnQuantityChanged;
	        
            CheckboxStackView.Hidden = hideCheckbox;
	        Quantity.Hidden = hideProductQuantity;

	        Product.PropertyChanged += OnQuantityChanged;
	        AddGestureToView(productSelected);
        }

	    private void OnQuantityChanged(object sender, PropertyChangedEventArgs e)
	    {
		    if (e.PropertyName == nameof(Product.Quantity))
		    {
			    Quantity.Text = Product.Quantity.ToString();
		    }
	    }

	    private void AddGestureToView(Action onProductSelected)
	    {		    
		    var tapGesture = new UITapGestureRecognizer();
		    tapGesture.DelaysTouchesBegan = true;

		    tapGesture.AddTarget(OnProductChecked);
		    tapGesture.AddTarget(onProductSelected);

		    ProductDetailsStack.AddGestureRecognizer(tapGesture);
	    }

        private void OnProductChecked()
        {
            Product.IsBought = !Product.IsBought;

            ToggleBoughtCheckbox(Product.IsBought);
        }
	    
	    public void SetVisualProperties()
        {
            BackgroundColor = Color.FromHex("#424242").ToUIColor();
            SelectedBackgroundView = new UIView { BackgroundColor = Color.FromHex("#FEE94E").ToUIColor() };

            Name.TextColor = Comment.TextColor = Quantity.TextColor = UIColor.White;

            Name.Font = UIFont.FromName("Quicksand-Medium", 20);
            Comment.Font = UIFont.FromName("Quicksand-Light", 15f);
            Quantity.Font = UIFont.FromName("Quicksand-Medium", 20f);

        }

	    private void ToggleBoughtCheckbox(bool isBought)
	    {
		    CheckboxView.Image = isBought ? UIImage.FromBundle("checked.png") : UIImage.FromBundle("unchecked.png");
	    }
    }
}
