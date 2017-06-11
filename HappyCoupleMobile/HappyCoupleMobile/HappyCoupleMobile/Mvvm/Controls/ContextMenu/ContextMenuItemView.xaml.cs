using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.ContextMenu
{
    public partial class ContextMenuItemView : TappableStackLayout
    {
        public bool IsItemDisabled { get; set; }

        public static readonly BindableProperty ContextMenuIconProperty =
            BindableProperty.Create(nameof(ContextMenuIcon), typeof(FileImageSource), typeof(ContextMenuItemView), null);

        public static readonly BindableProperty ContextMenuTextProperty =
                     BindableProperty.Create(nameof(ContextMenuText), typeof(string), typeof(ContextMenuItemView), string.Empty);

        public static readonly BindableProperty ItemTappedCommandProperty =
                      BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(ContextMenuItemView));

        public static readonly BindableProperty ItemTappedCommandParameterProperty =
                      BindableProperty.Create(nameof(ItemTappedCommandParameter), typeof(object), typeof(ContextMenuItemView));

        public FileImageSource ContextMenuIcon
        {
            get { return (FileImageSource)GetValue(ContextMenuIconProperty); }
            set { SetValue(ContextMenuIconProperty, value); }
        }

        public string ContextMenuText
        {
            get { return (string)GetValue(ContextMenuTextProperty); }
            set { SetValue(ContextMenuTextProperty, value); }
        }

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public object ItemTappedCommandParameter
        {
            get { return GetValue(ItemTappedCommandParameterProperty); }
            set { SetValue(ItemTappedCommandParameterProperty, value); }
        }

        public RelayCommand ItemCommandInternal => new RelayCommand(OnItemTapped);


        public ContextMenuItemView()
        {
            InitializeComponent();

            TappedCommand = ItemCommandInternal;
        }


        private void OnItemTapped()
        {
            if (IsItemDisabled || ItemTappedCommand == null || !ItemTappedCommand.CanExecute(ItemTappedCommandParameter))
            {
                return;
            }

            ItemTappedCommand.Execute(ItemTappedCommandParameter);
        }
    }
}
