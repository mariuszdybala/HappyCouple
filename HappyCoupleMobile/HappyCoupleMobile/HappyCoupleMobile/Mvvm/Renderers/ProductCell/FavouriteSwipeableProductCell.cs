using System.Collections.Generic;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Enums;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers.ProductCell
{
	public class FavouriteSwipeableProductCell : BaseSwipeableProductCell
	{
		public override IList<SwipeButton> RightSwipeButtons { get; set; }
		public override IList<SwipeButton> LeftSwipeButtons { get; set; } = new List<SwipeButton>();

		public FavouriteSwipeableProductCell()
		{
			CreateSwipeButtons();
		}

		private void CreateSwipeButtons()
		{
			var editButton = new SwipeButton
			{
				ButtonType = SwipeButtonType.Edit,
				Text = "Edytuj",
				Color = Color.FromHex("#4054B2"),
				ImageSource = (FileImageSource) Application.Current.Resources["EditList"]
			};

			var deleteButton = new SwipeButton
			{
				ButtonType = SwipeButtonType.Delete,
				Text = "Usuń",
				Color = Color.FromHex("#F6585D"),
				ImageSource = (FileImageSource) Application.Current.Resources["Delete"]
			};
			
			deleteButton.Clicked += DeleteButtonOnClicked;
			editButton.Clicked += EditButtonOnClicked;

			RightSwipeButtons = new List<SwipeButton> {editButton, deleteButton};
		}
		
		private void DeleteButtonOnClicked()
		{
			if (DeleteCommand != null && DeleteCommand.CanExecute(Product))
			{
				DeleteCommand.Execute(Product);
			}
		}

		private void EditButtonOnClicked()
		{
			if (EditCommand != null && EditCommand.CanExecute(Product))
			{
				EditCommand.Execute(Product);
			}
		}
	}
}
