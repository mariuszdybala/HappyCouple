using System.Collections.Generic;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Enums;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers.ProductCell
{
	public class FavouriteSwipeableProductCell : BaseSwipeableProductCell
	{
		public override IList<SwipeButton> SwipeButtons { get; set; }

		public FavouriteSwipeableProductCell()
		{
			CreateSwipeButtons();
		}

		private void CreateSwipeButtons()
		{
			var editButton = new SwipeButton
			{
				ButtonType = SwipeButtonType.Edit,
				Text = "Zmień",
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

			SwipeButtons = new List<SwipeButton> {editButton, deleteButton};
		}
	}
}
