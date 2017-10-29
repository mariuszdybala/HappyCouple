using System;
using System.Collections.Generic;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Enums;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers.ProductCell
{
	public class SwipeableProductCell : BaseSwipeableProductCell
	{
		public override IList<SwipeButton> SwipeButtons { get; set; }

		public SwipeableProductCell()
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
			
			deleteButton.Clicked += DeleteButtonOnClicked;
			editButton.Clicked += EditButtonOnClicked;

			
			SwipeButtons = new List<SwipeButton> {editButton, deleteButton};
		}

		private void DeleteButtonOnClicked()
		{
		}

		private void EditButtonOnClicked()
		{
		}
	}
}
