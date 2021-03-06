﻿using System;
using System.Windows.Input;
using HappyCoupleMobile.Mvvm.Controls.ContextMenu;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class ProductViewControl : ContextMenuLayout
    {
        public static BindableProperty ProductProperty =
	        BindableProperty.Create(nameof(Product), typeof(ProductVm), typeof(ProductViewControl), null, propertyChanged: OnProductChanged);
	    public static BindableProperty EditCommandProperty =
		    BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(ProductViewControl));
	    public static BindableProperty DeleteCommandProperty =
		    BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand), typeof(ProductViewControl));
	    public static BindableProperty SelectCommandProperty =
		    BindableProperty.Create(nameof(SelectCommand), typeof(ICommand), typeof(ProductViewControl));
	    public static BindableProperty CheckedCommandProperty =
		    BindableProperty.Create(nameof(CheckedCommand), typeof(ICommand), typeof(ProductViewControl));
	    public static BindableProperty TapCommandProperty =
		    BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(ProductViewControl));

	    public ICommand TapCommand
	    {
		    get => (ICommand) GetValue(TapCommandProperty);
		    set => SetValue(TapCommandProperty, value);
	    }
	    
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

	    public ICommand CheckedCommand
	    {
		    get => (ICommand) GetValue(CheckedCommandProperty);
		    set => SetValue(CheckedCommandProperty, value);
	    }

	    public ProductVm Product
        {
            get => (ProductVm)GetValue(ProductProperty);
		    set => SetValue(ProductProperty, value);
	    }

	    public bool ShowEditButton {set => EditMenuItem.IsVisible = value;}
	    public bool ShowSelectButton {set => SelectMenuItem.IsVisible = value;}
	    public bool ShowDeleteButton {set => DeleteMenuItem.IsVisible = value;}
	    public bool ShowCheckboxButton {set => IsBoughtCheckbox.IsVisible = value;}
	    public bool ShowQuantityButton {set => QuantityLabel.IsVisible = value;}

		public override ContextMenuView ContextMenu => ProductContextMenu;
        public override Xamarin.Forms.View DataContent => ProductData;

	    public ProductViewControl()
        {
            InitializeComponent();
        }

	    private static void OnProductChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
		    if (newvalue == null)
		    {
			    return;
		    }

		    var view = bindable as ProductViewControl;
		    var product = (ProductVm)newvalue;

		    view.CommentLabel.Text = product.Comment;
		    view.QuantityLabel.Text = $"Ilość: {product.Quantity}";
		    view.NameLabel.Text = product.Name;
	    }

	    public override void OnTapInternal()
	    {
		    if (TapCommand != null && TapCommand.CanExecute(Product))
		    {
			    TapCommand.Execute(Product);
		    }
	    }

        public void HideControlPanel()
        {
            CloseMenu();
        }
    }
}
