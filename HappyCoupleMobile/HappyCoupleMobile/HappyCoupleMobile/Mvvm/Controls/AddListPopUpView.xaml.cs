using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class AddListPopUpView : StackLayout
    {
        public static readonly BindableProperty AddNewListCommandProperty = BindableProperty.Create(nameof(AddNewListCommand),
        typeof(ICommand), typeof(AddListPopUpView));

        public static readonly BindableProperty CloseWindowCommandProperty = BindableProperty.Create(nameof(CloseWindowCommand),
        typeof(ICommand), typeof(AddListPopUpView));

        public ICommand AddNewListCommand
        {
            get { return (ICommand)GetValue(AddNewListCommandProperty); }
            set { SetValue(AddNewListCommandProperty, value); }
        }

        public ICommand CloseWindowCommand
        {
            get { return (ICommand)GetValue(CloseWindowCommandProperty); }
            set { SetValue(CloseWindowCommandProperty, value); }
        }

        public Command<string> AddNewListInternalCommand => new Command<string>(OnAddNewListInternal);
        public Command<Entry> EraseEntryInternalCommand => new Command<Entry>(OnEraseEntry);


        public AddListPopUpView()
        {
            InitializeComponent();
        }

        private void OnEraseEntry(Entry entry)
        {
            entry.Text = string.Empty;
        }

        private void OnAddNewListInternal(string listName)
        {
            if (string.IsNullOrWhiteSpace(listName))
            {
                ListNameMissingLabel.IsVisible = true;

                return;
            }

            ListNameMissingLabel.IsVisible = false;

            AddNewListCommand?.Execute(listName);
        }
    }
}
