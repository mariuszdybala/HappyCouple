using HappyCoupleMobile.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportCell(typeof(ViewCell), typeof(CustomViewCellRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = null
            };



            return cell;
        }
    }
}
