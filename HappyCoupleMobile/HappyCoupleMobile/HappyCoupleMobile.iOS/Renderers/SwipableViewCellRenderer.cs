
using System;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using SWTableViewCells;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HappyCoupleMobile.iOS.Controls;

[assembly:ExportRenderer(typeof(SwipableViewCell), typeof(SwipableViewCellRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
	public class SwipableViewCellRenderer : ViewCellRenderer
	{

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var formsCell = (SwipableViewCell)item;

            var swipeAbleNativeCell = reusableCell as ExtendedSwipeAbleCell;

            if (swipeAbleNativeCell == null)
			{
                swipeAbleNativeCell = RegisterCellIfNeeded(tv);
                swipeAbleNativeCell.SetVisualProperties();
                swipeAbleNativeCell.Delegate = new SWCellViewDelegate(formsCell);
			}

            if(formsCell != null)
            {
                swipeAbleNativeCell.UpdateCell(formsCell.Product, formsCell.OnProductChecked);

                var miscButton = new UIButton { BackgroundColor = UIColor.Red };
                miscButton.SetImage(UIImage.FromBundle("delete.png"), UIControlState.Normal);

                var deleteButton = new UIButton { BackgroundColor = Color.FromHex("#F6585D").ToUIColor() };
                deleteButton.SetImage(UIImage.FromBundle("delete.png"), UIControlState.Normal);
                deleteButton.SetTitle("Usuń", UIControlState.Normal);

                var editButton = new UIButton { BackgroundColor = Color.FromHex("#4054B2").ToUIColor() };
                editButton.SetImage(UIImage.FromBundle("edit.png"), UIControlState.Normal);
                editButton.SetTitle("Edytuj", UIControlState.Normal);

                swipeAbleNativeCell.RightUtilityButtons = new UIButton[] 
                { 
                    deleteButton, 
                    editButton
                };
            }

            tv.EstimatedRowHeight = UITableView.AutomaticDimension;
            tv.RowHeight = UITableView.AutomaticDimension;

            //tv.RowHeight = swipeAbleNativeCell.GetContentHeight();

            tv.AllowsSelection = false;

            swipeAbleNativeCell.SetNeedsUpdateConstraints();
            swipeAbleNativeCell.UpdateConstraintsIfNeeded();

            return swipeAbleNativeCell;
		}

        private ExtendedSwipeAbleCell RegisterCellIfNeeded(UITableView tv)
        {
            var cell = (ExtendedSwipeAbleCell)tv.DequeueReusableCell("ExtendedSwipeAbleCellId");

            if (cell == null)
            {
                tv.RegisterNibForCellReuse(ExtendedSwipeAbleCell.Nib, "ExtendedSwipeAbleCellId");
                cell = (ExtendedSwipeAbleCell)tv.DequeueReusableCell("ExtendedSwipeAbleCellId");
            }

            return cell;
        }
	}

	// ReSharper disable once InconsistentNaming
	public class SWCellViewDelegate : SWTableViewCellDelegate
	{
		private readonly SwipableViewCell _swipableViewCell;

		public SWCellViewDelegate(SwipableViewCell swipableViewCell)
		{
			_swipableViewCell = swipableViewCell;
		}

		public override void DidTriggerLeftUtilityButton(SWTableViewCell cell, nint index)
		{
		}

		public override void DidTriggerRightUtilityButton(SWTableViewCell cell, nint index)
		{
		}
	}
}
