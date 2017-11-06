using System.Collections.Generic;
using System.Windows.Input;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers.ProductCell
{
	public abstract class BaseSwipeableProductCell : ViewCell
	{
		public bool ShowCheckbox { get; set; }
		public bool ShowProductQuantity { get; set; }
		public bool IsTapable { get; set; } = true;

		public abstract IList<SwipeButton> RightSwipeButtons { get; set;}
		public abstract IList<SwipeButton> LeftSwipeButtons { get; set;}

        public static BindableProperty ProductProperty =
            BindableProperty.Create(nameof(Product), typeof(ProductVm), typeof(BaseSwipeableProductCell));
		public static BindableProperty EditCommandProperty =
			BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(BaseSwipeableProductCell));
		public static BindableProperty DeleteCommandProperty =
			BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand), typeof(BaseSwipeableProductCell));
		public static BindableProperty SelectCommandProperty =
			BindableProperty.Create(nameof(SelectCommand), typeof(ICommand), typeof(BaseSwipeableProductCell));

		public ICommand EditCommand
		{
			get => (ICommand) GetValue(EditCommandProperty);
			set => SetValue(EditCommandProperty, value);
		}

		public ICommand DeleteCommand
		{
			get => (ICommand) GetValue(DeleteCommandProperty);
			set => SetValue(DeleteCommandProperty, value);
		}

		public ICommand SelectCommand
		{
			get => (ICommand) GetValue(SelectCommandProperty);
			set => SetValue(SelectCommandProperty, value);
		}

        public ProductVm Product
        {
            get => (ProductVm)GetValue(ProductProperty);
            set => SetValue(ProductProperty, value);
        }

		public void OnProductSelected()
        {
	        if (SelectCommand != null && SelectCommand.CanExecute(Product))
	        {
		        SelectCommand.Execute(Product);
	        }
        }
	}
}
