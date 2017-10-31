using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Helpers;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ShoppingListPanel : Frame
    {
        public static readonly BindableProperty ShoppingListProperty = BindableProperty.Create(
        nameof(ShoppingList), typeof(ShoppingListVm), typeof(ShoppingListPanel));

        public static readonly BindableProperty AddCommandProperty = BindableProperty.Create(
        nameof(AddCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
        nameof(DeleteCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
        nameof(CloseCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty EditOrListTappedCommandProperty = BindableProperty.Create(
        nameof(EditOrListTappedCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public ShoppingListVm ShoppingList
        {
            get { return (ShoppingListVm)GetValue(ShoppingListProperty); }
            set { SetValue(ShoppingListProperty, value); }
        }

        public ICommand AddCommand
        {
            get { return (ICommand)GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }
        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        public ICommand EditOrListTappedCommand
        {
            get { return (ICommand)GetValue(EditOrListTappedCommandProperty); }
            set { SetValue(EditOrListTappedCommandProperty, value); }
        }

        public ShoppingListPanel()
        {
            InitializeComponent();
        }

        

        private void OnAdd(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            image.SetAnimation();

            if (AddCommand != null && AddCommand.CanExecute(ShoppingList))
            {
                AddCommand.Execute(ShoppingList);
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            image.SetAnimation();

            if (CloseCommand != null && CloseCommand.CanExecute(ShoppingList))
            {
                CloseCommand.Execute(ShoppingList);
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            image.SetAnimation();

            if (DeleteCommand != null && DeleteCommand.CanExecute(ShoppingList))
            {
                DeleteCommand.Execute(ShoppingList);
            }
        }

        private void OnShoppingListTapped(object sender, EventArgs e)
        {
            Frame grid = (Frame)sender;
            grid.SetAnimation(0.95, 120);

            if (EditOrListTappedCommand != null && EditOrListTappedCommand.CanExecute(ShoppingList))
            {
                EditOrListTappedCommand.Execute(ShoppingList);
            }
        }
    }
}
