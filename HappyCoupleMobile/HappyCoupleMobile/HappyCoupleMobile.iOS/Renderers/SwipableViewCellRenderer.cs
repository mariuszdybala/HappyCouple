
using System;
using System.Collections.Generic;
using System.Linq;
using HappyCoupleMobile.Custom;
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
		enum SwipeButtonDirection
		{
			Right,
			Left
		}
		
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
                swipeAbleNativeCell.UpdateCell(formsCell.Product, formsCell.OnProductSelected, formsCell.IsTapable, !formsCell.ShowCheckbox, !formsCell.ShowProductQuantity);
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
			if(!swipeableFormsCell.IsTapable)
			{
				return;
			}

			AddSwipeButtons(SwipeButtonDirection.Right, swipeableNativeCell, swipeableFormsCell.RightSwipeButtons);
			AddSwipeButtons(SwipeButtonDirection.Left, swipeableNativeCell, swipeableFormsCell.LeftSwipeButtons);
		}

		private static void AddSwipeButtons(SwipeButtonDirection buttonType, ExtendedSwipeAbleCell swipeableNativeCell, IList<SwipeButton> buttons)
		{
			var swipeableButtonsCount = buttons.Count();
			var swipeButtons = new UIButton[swipeableButtonsCount];

			for (int i = 0; i < swipeableButtonsCount; i++)
			{
				var formsButton = buttons[i];

				var uiButton = new UIButton {BackgroundColor = formsButton.Color.ToUIColor()};
				uiButton.SetImage(UIImage.FromBundle(formsButton.ImageSource.File), UIControlState.Normal);
				uiButton.SetTitle(formsButton.Text, UIControlState.Normal);

				swipeButtons[i] = uiButton;
			}

			if (buttonType == SwipeButtonDirection.Right)
			{
				swipeableNativeCell.RightUtilityButtons = swipeButtons;
			}
			else
			{
				swipeableNativeCell.LeftUtilityButtons = swipeButtons;
			}
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
			_baseSwipeableProductCell.LeftSwipeButtons[(int)index].Clicked.Invoke();
			cell.HideUtilityButtons(true);
		}

		public override void DidTriggerRightUtilityButton(SWTableViewCell cell, nint index)
		{
			_baseSwipeableProductCell.RightSwipeButtons[(int)index].Clicked.Invoke();
			cell.HideUtilityButtons(true);
		}
	}
}
