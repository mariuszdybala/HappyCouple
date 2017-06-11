using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace HappyCoupleMobile.Mvvm.Controls.ContextMenu
{
    public partial class ContextMenuView : StackLayout
    {
        public int MenuVisibleItemsCount => Children.Count(x => x.IsVisible);

        public ContextMenuView()
        {
            InitializeComponent();
        }
    }
}
