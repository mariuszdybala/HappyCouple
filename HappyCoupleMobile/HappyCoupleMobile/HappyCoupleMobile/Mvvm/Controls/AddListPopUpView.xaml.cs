using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class AddListPopUpView : Frame
    {

        public static readonly BindableProperty ListNameProperty = BindableProperty.Create(
        nameof(ShoppingList), typeof(string), typeof(AddListPopUpView), propertyChanged: OnListNameChanged);

        public string ListName
        {
            get { return (string)GetValue(ListNameProperty); }
            set { SetValue(ListNameProperty, value); }
        }

        public AddListPopUpView()
        {
            InitializeComponent();
        }

        private static void OnListNameChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue)
            {
                return;
            }

            var listName = (string)newvalue;
            var addListPopUpView = (AddListPopUpView)bindable;
        }
    }
}
