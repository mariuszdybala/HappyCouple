using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappyCoupleMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            var h = Ubrania.Height;
            var w = Ubrania.Width;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }
    }
}
