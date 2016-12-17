using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Helpers;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ShoppingListPanel : Frame
    {
        public static readonly BindableProperty ShoppingListProperty = BindableProperty.Create(
        nameof(ShoppingList), typeof(ShoppingList), typeof(ShoppingListPanel), propertyChanged: OnShoppingListChanged);

        public static readonly BindableProperty AddCommandProperty = BindableProperty.Create(
        nameof(AddCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
        nameof(DeleteCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
        nameof(CloseCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty EditOrListTappedCommandProperty = BindableProperty.Create(
        nameof(EditOrListTappedCommand), typeof(ICommand), typeof(ShoppingListPanel), defaultBindingMode: BindingMode.OneWay);

        public ShoppingList ShoppingList
        {
            get { return (ShoppingList)GetValue(ShoppingListProperty); }
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

        private static void OnShoppingListChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var shoppingList = (ShoppingList)newvalue;
            var shoppingListPanel = (ShoppingListPanel)bindable;

            if (shoppingList.Products.Any())
            {
                AddProductTypesToProductTypesContainer(shoppingList.Products, shoppingListPanel);
            }
        }

        private static void AddProductTypesToProductTypesContainer(List<Product> products, ShoppingListPanel shoppingListPanel)
        {
            //var types = products.GroupBy(x => x.ProductType).Select(x=>x.Key).ToList();

//            foreach (var type in types)
//            {
//                FileImageSource imageSource = Application.Current.Resources[type.ToString()] as FileImageSource;
//
//                if (imageSource == null)
//                {
//                    continue;
//                }
//
//                shoppingListPanel.ProductTypesContainer.Children.Add(new Image {Source = imageSource, HeightRequest = 15});
//
//                if (shoppingListPanel.ProductTypesContainer.Children.Count == 9)
//                {
//                    shoppingListPanel.ProductTypesContainer.Children.Add(new Label
//                    {
//                        TextColor = Color.Gray,
//                        Text = "..."
//                    });
//                    return;
//                }
//            }
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
