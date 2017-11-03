using System;

using Foundation;
using UIKit;
using HappyCoupleMobile.Model;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace HappyCoupleMobile.iOS.Controls
{
    public partial class ProductGroupingCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ProductGroupingCell");
        public static readonly UINib Nib;

        static ProductGroupingCell()
        {
            Nib = UINib.FromName("ProductGroupingCell", NSBundle.MainBundle);
        }

        protected ProductGroupingCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void UpdateCell(ProductType product, bool showChevronImage)
        {
            ProductTypeNameLabel.Text = product.Type;

            ChevronImage.Hidden = !showChevronImage;

            var imageFileName = (FileImageSource)Xamarin.Forms.Application.Current.Resources[product.IconName];
            ProductTypeImage.Image = UIImage.FromBundle(imageFileName.File);
        }

        public void SetVisualProperties()
        {

            SelectedBackgroundView = new UIView { BackgroundColor = Color.FromHex("#4054B2").ToUIColor() };

            ProductTypeNameLabel.TextColor = UIColor.White;

            ProductTypeNameLabel.Font = UIFont.FromName("Quicksand-Medium", 17);
        }
    }
}
