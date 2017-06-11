using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ChooseProductPopUpView : StackLayout
    {
        public Command<Entry> EraseEntryInternalCommand => new Command<Entry>(OnEraseEntry);

        public ChooseProductPopUpView()
        {
            InitializeComponent();
        }

        private void OnEraseEntry(Entry entry)
        {
            entry.Text = string.Empty;
        }
    }
}
