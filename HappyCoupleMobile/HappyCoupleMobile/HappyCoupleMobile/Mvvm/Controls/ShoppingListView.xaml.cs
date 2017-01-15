using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HappyCoupleMobile.Model;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ShoppingListView : StackLayout
    {
        private bool _isInitialized;

        public static readonly BindableProperty ShoppingListSourceProperty = BindableProperty.Create(
        nameof(ShoppingListSource), typeof(ObservableCollection<ShoppingList>), typeof(ShoppingListView), propertyChanged: OnShoppingListSourceChanged);

        public static readonly BindableProperty AddCommandProperty = BindableProperty.Create(
        nameof(AddCommand), typeof(ICommand), typeof(ShoppingListView));

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
        nameof(DeleteCommand), typeof(ICommand), typeof(ShoppingListView));

        public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
        nameof(CloseCommand), typeof(ICommand), typeof(ShoppingListView));

        public static readonly BindableProperty EditOrListTappedCommandProperty = BindableProperty.Create(
        nameof(EditOrListTappedCommand), typeof(ICommand), typeof(ShoppingListView));

        public ObservableCollection<ShoppingList> ShoppingListSource
        {
            get { return (ObservableCollection<ShoppingList>)GetValue(ShoppingListSourceProperty); }
            set { SetValue(ShoppingListSourceProperty, value); }
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

        public ShoppingListView()
        {
            InitializeComponent();
        }

        private static void ShoppingLists_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs arg)
        {
        }

        private static void OnShoppingListSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var shoppingLists = (ObservableCollection<ShoppingList>)newvalue;
            var shoppingListView = (ShoppingListView)bindable;

            if (!shoppingListView._isInitialized)
            {
                shoppingListView.ShoppingListSource.CollectionChanged += ShoppingLists_CollectionChanged;
            }

            shoppingListView.FeedShoppingListContainer(shoppingLists);

            shoppingListView._isInitialized = true;
        }

        public void FeedShoppingListContainer(IList<ShoppingList> shoppingLists) 
        {
            if (!shoppingLists.Any())
            {
                return;
            }

            if (ShoppingListPanelContent.Children.Any())
            {
                ShoppingListPanelContent.Children.Clear();
            }

            foreach (ShoppingList shoppingList in shoppingLists)
            {
                ShoppingListPanelContent.Children.Add(new ShoppingListPanel
                {
                    ShoppingList = shoppingList,
                    AddCommand = AddCommand,
                    CloseCommand = CloseCommand,
                    DeleteCommand = DeleteCommand,
                    EditOrListTappedCommand = EditOrListTappedCommand
                });
            }

        }
    }
}
