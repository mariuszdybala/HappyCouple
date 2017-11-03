using System;
using HappyCoupleMobile.iOS.Controls;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers.ProductCell;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseProductGroupingCell), typeof(ProductGroupingCellRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
    public class ProductGroupingCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var formsCell = (BaseProductGroupingCell)item;

            var productGroupingCell = reusableCell as ProductGroupingCell;

            if (productGroupingCell == null)
            {
                productGroupingCell = RegisterCellIfNeeded(tv);
                productGroupingCell.SetVisualProperties();
            }

            productGroupingCell.UpdateCell(formsCell.ProductType, formsCell.ShowChevron);

            return productGroupingCell;
        }

        private ProductGroupingCell RegisterCellIfNeeded(UITableView tv)
        {
            var cell = (ProductGroupingCell)tv.DequeueReusableCell("ProductGroupingCellId");

            if (cell == null)
            {
                tv.RegisterNibForCellReuse(ProductGroupingCell.Nib, "ProductGroupingCellId");
                cell = (ProductGroupingCell)tv.DequeueReusableCell("ProductGroupingCellId");
            }

            return cell;
        }
    }
}
