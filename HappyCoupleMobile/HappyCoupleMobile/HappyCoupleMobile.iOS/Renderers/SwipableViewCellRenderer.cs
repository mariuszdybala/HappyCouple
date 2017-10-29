
using System;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using SWTableViewCells;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HappyCoupleMobile.iOS.Controls;
using HappyCoupleMobile.Mvvm.Renderers.ProductCell;

[assembly:ExportRenderer(typeof(BaseSwipeableProductCell), typeof(SwipableViewCellRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
	public class SwipableViewCellRenderer : ViewCellRenderer
	{

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var formsCell = (BaseSwipeableProductCell)item;

            var swipeAbleNativeCell = reusableCell as ExtendedSwipeAbleCell;

            if (swipeAbleNativeCell == null)
			{
                swipeAbleNativeCell = RegisterCellIfNeeded(tv);
                swipeAbleNativeCell.SetVisualProperties();
                swipeAbleNativeCell.Delegate = new SWCellViewDelegate(formsCell);
			}

            if(formsCell != null)
            {
                swipeAbleNativeCell.UpdateCell(formsCell.Product, formsCell.OnProductSelected, !formsCell.ShowCheckbox, !formsCell.ShowProductQuantity);

	            CreateAndAddSwipeButtons(swipeAbleNativeCell, formsCell);
            }

            tv.EstimatedRowHeight = UITableView.AutomaticDimension;
            tv.RowHeight = UITableView.AutomaticDimension;

            tv.AllowsSelection = false;

            swipeAbleNativeCell.SetNeedsUpdateConstraints();
            swipeAbleNativeCell.UpdateConstraintsIfNeeded();

            return swipeAbleNativeCell;
		}

		private void CreateAndAddSwipeButtons(ExtendedSwipeAbleCell swipeableNativeCell, BaseSwipeableProductCell swipeableFormsCell)
		{
			var swipeableButtonsCount = swipeableFormsCell.SwipeButtons.Count;
			var buttons = new UIButton[swipeableButtonsCount];

			for (int i = 0; i < swipeableButtonsCount; i++)
			{
				var formsButton = swipeableFormsCell.SwipeButtons[i];
				
				  var uiButton = new UIButton { BackgroundColor = formsButton.Color.ToUIColor() };
				uiButton.SetImage(UIImage.FromBundle(formsButton.ImageSource.File), UIControlState.Normal);
				uiButton.SetTitle(formsButton.Text, UIControlState.Normal);

				buttons[i] = uiButton;
			}

			swipeableNativeCell.RightUtilityButtons = buttons;

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
		private readonly BaseSwipeableProductCell _baseSwipeableProductCell;

		public SWCellViewDelegate(BaseSwipeableProductCell baseSwipeableProductCell)
		{
			_baseSwipeableProductCell = baseSwipeableProductCell;
		}

		public override void DidTriggerLeftUtilityButton(SWTableViewCell cell, nint index)
		{
		}

		public override void DidTriggerRightUtilityButton(SWTableViewCell cell, nint index)
		{
			_baseSwipeableProductCell.SwipeButtons[(int)index].Clicked.Invoke();
		}
	}
}
