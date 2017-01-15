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
    public partial class AddListPopUpView : Frame
    {
        public static readonly BindableProperty AddNewListCommandProperty = BindableProperty.Create(nameof(AddNewListCommand),
        typeof(ICommand), typeof(AddListPopUpView));

        public ICommand AddNewListCommand
        {
            get { return (ICommand)GetValue(AddNewListCommandProperty); }
            set { SetValue(AddNewListCommandProperty, value); }
        }

        public AddListPopUpView()
        {
            InitializeComponent();
        }
    }
}
