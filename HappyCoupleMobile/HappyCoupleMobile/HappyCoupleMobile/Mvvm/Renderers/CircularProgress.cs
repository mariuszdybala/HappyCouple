using HappyCoupleMobile.Mvvm.Controls;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers
{
    public class CircularProgress : StackLayout
    {
        public static BindableProperty PercentProperty = BindableProperty
        .Create(nameof(Percent), typeof(int), typeof(CircularProgress), 0);

        public int Percent
        {
            get { return (int)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }
    }
}