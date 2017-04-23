using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HappyCoupleMobile.Model;
using System.Windows.Input;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Helpers;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ShoppingListView : StackLayout
    {
        public ShoppingListTabType ShoppingListTabType { get; set; }

        public static readonly BindableProperty ShoppingListSourceProperty = BindableProperty.Create(
        nameof(ShoppingListSource), typeof(ObservableCollection<ShoppingListVm>), typeof(ShoppingListView), propertyChanged: OnShoppingListSourceChanged);

        public static readonly BindableProperty AddCommandProperty = BindableProperty.Create(
        nameof(AddCommand), typeof(ICommand), typeof(ShoppingListView));

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
        nameof(DeleteCommand), typeof(ICommand), typeof(ShoppingListView));

        public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
        nameof(CloseCommand), typeof(ICommand), typeof(ShoppingListView));

        public static readonly BindableProperty EditOrListTappedCommandProperty = BindableProperty.Create(
        nameof(EditOrListTappedCommand), typeof(ICommand), typeof(ShoppingListView));

        public static readonly BindableProperty AddShoppingListCommandProperty = BindableProperty.Create(
        nameof(AddShoppingListCommand), typeof(ICommand), typeof(ShoppingListView));

        public ObservableCollection<ShoppingListVm> ShoppingListSource
        {
            get { return (ObservableCollection<ShoppingListVm>)GetValue(ShoppingListSourceProperty); }
            set
            {
                SetValue(ShoppingListSourceProperty, value);
            }
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
        public ICommand AddShoppingListCommand
        {
            get { return (ICommand)GetValue(AddShoppingListCommandProperty); }
            set { SetValue(AddShoppingListCommandProperty, value); }
        }

        public ShoppingListView()
        {
            InitializeComponent();

        }

        private void ShoppingLists_CollectionChanged(object sender, NotifyCollectionChangedEventArgs arg)
        {
            if (arg.Action == NotifyCollectionChangedAction.Add)
            {
                var newShoppingList = (ShoppingListVm)arg.NewItems[0];
                InsertNewShoppingListPanel(newShoppingList);
            }

            else if (arg.Action == NotifyCollectionChangedAction.Remove)
            {
                var shoppingListToDelete = (ShoppingListVm)arg.OldItems[0];
                DeleteShoppingListPanel(shoppingListToDelete);
            }
        }

        private static void OnShoppingListSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var shoppingLists = (ObservableCollection<ShoppingListVm>)newvalue;
            var shoppingListView = (ShoppingListView)bindable;

            InitialShoppingList(shoppingLists, shoppingListView);
        }

        private static void InitialShoppingList(ObservableCollection<ShoppingListVm> shoppingLists, ShoppingListView shoppingListView)
        {
            shoppingListView.UnSubscribeEventsToProductList(shoppingLists);

            if (!shoppingLists.Any())
            {
                return;
            }

            if (shoppingListView.ShoppingListPanelContent.Children.Any())
            {
                shoppingListView.ShoppingListPanelContent.Children.Clear();
            }

            foreach (ShoppingListVm shoppingList in shoppingLists)
            {
                shoppingListView.InsertNewShoppingListPanel(shoppingList);
            }

            shoppingListView.AssingEventsToProductList(shoppingLists);
        }

        private void InsertNewShoppingListPanel(ShoppingListVm shoppingList)
        {
            ShoppingListPanelContent.Children.Insert(0, new ShoppingListPanel
            {
                ShoppingList = shoppingList,
                AddCommand = AddCommand,
                CloseCommand = CloseCommand,
                DeleteCommand = DeleteCommand,
                EditOrListTappedCommand = EditOrListTappedCommand
            });

            SetEmptyListPlaceholderStackVisibility();
        }

        private void DeleteShoppingListPanel(ShoppingListVm shoppingListToDelete)
        {
            var shoppingListViewToDelete = ShoppingListPanelContent.Children.OfType<ShoppingListPanel>().First(x => x.ShoppingList.Id == shoppingListToDelete.Id);

            ShoppingListPanelContent.Children.Remove(shoppingListViewToDelete);

            SetEmptyListPlaceholderStackVisibility();
        }

        public void AssingEventsToProductList(ObservableCollection<ShoppingListVm> shoppingLists)
        {
            shoppingLists.CollectionChanged += ShoppingLists_CollectionChanged;
        }

        public void UnSubscribeEventsToProductList(ObservableCollection<ShoppingListVm> shoppingLists)
        {
            shoppingLists.CollectionChanged -= ShoppingLists_CollectionChanged;
        }

        private void SetEmptyListPlaceholderStackVisibility()
        {
            EmptyListPlaceholderStack.IsVisible = !ShoppingListPanelContent.Children.OfType<ShoppingListPanel>().Any();

            AddListLabelPlaceholder.IsVisible = AddListImagePlaceholder.IsVisible =
                EmptyListPlaceholderStack.IsVisible 
                && ShoppingListTabType == ShoppingListTabType.Active;
        }
    }
}
