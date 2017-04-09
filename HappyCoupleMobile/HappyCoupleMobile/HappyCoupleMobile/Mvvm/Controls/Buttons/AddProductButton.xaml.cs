using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Helpers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.Buttons
{
    public partial class AddProductButton : Frame
    {
        public static BindableProperty AddProductButtonClickedCommandProperty = BindableProperty.Create
             (nameof(AddProductButtonClickedCommand), typeof(ICommand), typeof(AddProductButton), null);

        public ICommand AddProductButtonClickedCommand
        {
            get { return (ICommand)GetValue(AddProductButtonClickedCommandProperty); }
            set { SetValue(AddProductButtonClickedCommandProperty, value); }
        }

        public ICommand AddProductButtonClickedIntenalCommand { get; set; }

        public AddProductButton()
        {
            InitializeComponent();

            AddProductButtonClickedIntenalCommand = new Command(async () => await OnAddProductButtonClickedIntenal());
        }

        private async Task OnAddProductButtonClickedIntenal()
        {
            await this.SetAnimation(0.95, 120);

            AddProductButtonClickedCommand?.Execute(null);
        }
    }
}
