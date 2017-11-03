using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.View.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.View
{
    public partial class ShoppingsView : BaseHappyContentPage
    {
        public static readonly BindableProperty ShowActiveProperty = BindableProperty.Create(
        nameof(ShowActive), typeof(bool), typeof(ShoppingsView), defaultValue: true);

        public static readonly BindableProperty ShowClosedProperty = BindableProperty.Create(
        nameof(ShowClosed), typeof(bool), typeof(ShoppingsView), defaultValue: false);

        public static readonly BindableProperty ActiveTabTappedCommandProperty = BindableProperty.Create(
        nameof(ActiveTabTappedCommand), typeof(ICommand), typeof(ShoppingsView));

        public static readonly BindableProperty ClosedTabTappedCommandProperty = BindableProperty.Create(
        nameof(ClosedTabTappedCommand), typeof(ICommand), typeof(ShoppingsView));

        public bool ShowActive
        {
            get { return (bool)GetValue(ShowActiveProperty); }
            set { SetValue(ShowActiveProperty, value); }
        }

        public bool ShowClosed
        {
            get { return (bool)GetValue(ShowClosedProperty); }
            set { SetValue(ShowClosedProperty, value); }
        }

        public ICommand ActiveTabTappedCommand
        {
            get { return (ICommand)GetValue(ActiveTabTappedCommandProperty); }
            set { SetValue(ActiveTabTappedCommandProperty, value); }
        }

        public ICommand ClosedTabTappedCommand
        {
            get { return (ICommand)GetValue(ClosedTabTappedCommandProperty); }
            set { SetValue(ClosedTabTappedCommandProperty, value); }
        }

        public ShoppingsView()
        {
            InitializeComponent();

            ActiveTabTappedCommand = new RelayCommand(OnActiveTabTappedCommand);
            ClosedTabTappedCommand = new RelayCommand(OnClosedTabTappedCommand);
        }

        private void OnClosedTabTappedCommand()
        {
            SwitchTabs(ShoppingListStatus.Closed);
        }

        private void OnActiveTabTappedCommand()
        {
            SwitchTabs(ShoppingListStatus.Active);
        }

        private void SwitchTabs(ShoppingListStatus status)
        {
	        if (status == ShoppingListStatus.Active && ShowActive)
	        {
		        return;
	        }

	        if (status == ShoppingListStatus.Closed && ShowClosed)
	        {
		        return;
	        }

            ShowActive = !ShowActive;
            ShowClosed = !ShowClosed;

            SwitchTabsStyles(ShowActive, ActiveTabPanel, ActiveTabLabel);
            SwitchTabsStyles(ShowClosed, ClosedTabPanel, ClosedTabLabel);
        }

        private void SwitchTabsStyles(bool isOnTop, StackLayout tabStack, Label tabLabel)
        {
            tabStack.BackgroundColor = isOnTop ? (Color)Application.Current.Resources["SecondColor"] : Color.White;

            tabLabel.TextColor = isOnTop ? Color.White : (Color)Application.Current.Resources["SecondColor"];
        }
    }
}
