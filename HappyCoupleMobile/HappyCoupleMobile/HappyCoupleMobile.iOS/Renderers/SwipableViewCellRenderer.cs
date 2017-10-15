
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
                swipeAbleNativeCell.UpdateCell(formsCell.ProductName, formsCell.Comment, formsCell.Quentity);
                swipeAbleNativeCell.RightUtilityButtons = new UIButton[] { new UIButton { BackgroundColor = UIColor.Blue }, new UIButton { BackgroundColor = UIColor.Red } };
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
