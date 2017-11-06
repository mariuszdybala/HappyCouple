using System;
using System.Collections.Generic;
using System.Windows.Input;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Enums;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers.ProductCell
{
	public class SwipeableProductCell : BaseSwipeableProductCell
	{
		public override IList<SwipeButton> RightSwipeButtons { get; set; }
		public override IList<SwipeButton> LeftSwipeButtons { get; set; }
		
		public static BindableProperty ChangeQuantityCommandProperty =
			BindableProperty.Create(nameof(ChangeQuantityCommand), typeof(ICommand), typeof(SwipeableProductCell));

		public ICommand ChangeQuantityCommand
		{
			get => (ICommand) GetValue(ChangeQuantityCommandProperty);
			set => SetValue(ChangeQuantityCommandProperty, value);
		}

		public SwipeableProductCell()
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
			
			var changeQuantityButton = new SwipeButton
			{
				ButtonType = SwipeButtonType.Edit,
				Text = "Ilość",
				Color = Color.FromHex("#30AD63"),
				ImageSource = (FileImageSource) Application.Current.Resources["ChangeQuantity"]
			};
			
			changeQuantityButton.Clicked += ChangeQuantityClicked;
			
			LeftSwipeButtons = new List<SwipeButton> {changeQuantityButton};
		}
		
		private void ChangeQuantityClicked()
		{
			if (ChangeQuantityCommand != null && ChangeQuantityCommand.CanExecute(Product))
			{
				ChangeQuantityCommand.Execute(Product);
			}
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
