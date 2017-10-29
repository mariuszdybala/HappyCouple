using System;
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
            get { return _product; }
            set
            {
                Name.Text = value.Name;
                Comment.Text = value.Comment;
                Quantity.Text = value.Quantity.ToString();
                _product = value;
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

        public void UpdateCell(ProductVm product, Action productSelected, bool hideCheckbox = false, bool hideProductQuantity = false)
        {
            Product = product;
            CheckboxStackView.Hidden = hideCheckbox;
	        Quantity.Hidden = hideProductQuantity;

	        AddGestureToView(productSelected);
        }

	    private void AddGestureToView(Action onProductSelected)
	    {		    
		    var tapGesture = new UITapGestureRecognizer();

		    tapGesture.AddTarget(OnProductChecked);
		    tapGesture.AddTarget(onProductSelected);

		    this.AddGestureRecognizer(tapGesture);
	    }

        private void OnProductChecked()
        {
            Product.IsBought = !Product.IsBought;

            CheckboxView.Image = Product.IsBought ? UIImage.FromBundle("checked.png") : UIImage.FromBundle("unchecked.png");
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
    }
}
