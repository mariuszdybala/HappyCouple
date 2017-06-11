using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.ContextMenu
{
    public abstract class ContextMenuLayout : GestureStackLayout
    {
		protected bool IsContextMenuVisible { get; private set; }

		public abstract ContextMenuView ContextMenu { get; }
        public abstract Xamarin.Forms.View DataContent { get; }

		public abstract void OnTapInternal();

		public static BindableProperty EnableSwipeMenuProperty =
					  BindableProperty.Create(nameof(EnableSwipeMenu), typeof(bool), typeof(ContextMenuLayout), defaultValue: true);
		public static BindableProperty TappedCommandProperty =
					  BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(ContextMenuLayout));

		public bool EnableSwipeMenu
		{
			get { return (bool)GetValue(EnableSwipeMenuProperty); }
			set { SetValue(EnableSwipeMenuProperty, value); }
		}

		public ICommand TappedCommand
		{
			get { return (ICommand)GetValue(TappedCommandProperty); }
			set { SetValue(TappedCommandProperty, value); }
		}

		public void CloseMenu()
		{
			OnSwipeRight();
		}

		public override void OnTap()
		{
			base.OnTap();
			if (IsContextMenuVisible)
			{
				return;
			}

			OnTapInternal();
		}

		public override void OnSwipeLeft()
		{
			if (!EnableSwipeMenu)
			{
				return;
			}

			base.OnSwipeLeft();

			ChangeInputTransparentAllContextMenuItems(false);
			ChangeDisableAllContextMenuItems(false);

			IsContextMenuVisible = true;

			DataContent.TranslateTo(GetTranslationValue(), 0, 150, Easing.Linear);
		}

		public override void OnSwipeRight()
		{
			if (!EnableSwipeMenu)
			{
				return;
			}

			base.OnSwipeRight();

			ChangeInputTransparentAllContextMenuItems(true);
			ChangeDisableAllContextMenuItems(true);

			IsContextMenuVisible = false;

			DataContent.TranslateTo(0, 0, 150, Easing.Linear);
		}

		private double GetTranslationValue()
		{
			return ContextMenu.MenuVisibleItemsCount * 100 * -1;
		}

		private void ChangeDisableAllContextMenuItems(bool isDisabled)
		{
			foreach (var item in GetContextMenuItems())
			{
				item.IsItemDisabled = isDisabled;
			}
		}

		private void ChangeInputTransparentAllContextMenuItems(bool inputTransparentValue)
		{
			foreach (var item in GetContextMenuItems())
			{
				item.InputTransparent = inputTransparentValue;
				var icon = item.Children.FirstOrDefault();

				if (icon == null)
				{
					continue;
				}

				icon.InputTransparent = inputTransparentValue;
			}
		}

		private IList<ContextMenuItemView> GetContextMenuItems()
		{
			return ContextMenu.Children.OfType<ContextMenuItemView>().ToList();
		}
    }
}
