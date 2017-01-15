using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class TappableLabel : Label
    {
        public static BindableProperty TappedCommandProperty =
            BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(TappableLabel));

        public static BindableProperty TappedCommandParameterProperty =
            BindableProperty.Create(nameof(TappedCommandParameter), typeof(object), typeof(TappableLabel));

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public object TappedCommandParameter
        {
            get { return GetValue(TappedCommandParameterProperty); }
            set { SetValue(TappedCommandParameterProperty, value); }
        }

        public TappableLabel()
        {
            InitializeComponent();
        }
    }
}
