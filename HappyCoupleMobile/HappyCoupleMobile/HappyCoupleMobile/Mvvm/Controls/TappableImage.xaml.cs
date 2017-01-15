using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Helpers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class TappableImage : Image
    {
        public static BindableProperty TappedCommandProperty =
            BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(TappableImage));

        public static BindableProperty TappedCommandParameterProperty =
            BindableProperty.Create(nameof(TappedCommandParameter), typeof(object), typeof(TappableImage));

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

        public ICommand TransitionCommand { get; set; }

        public TappableImage()
        {
            InitializeComponent();

            TransitionCommand = new RelayCommand(OnImageTapped);
        }

        private void OnImageTapped()
        {
            AnimateImage();
            TappedCommand?.Execute(TappedCommandParameter);
        }

        public void AnimateImage()
        {
            this.SetAnimation(0.95, 120);
        }
    }
}
