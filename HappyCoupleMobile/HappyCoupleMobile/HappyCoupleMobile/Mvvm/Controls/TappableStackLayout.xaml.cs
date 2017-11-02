using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Helpers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class TappableStackLayout : StackLayout
    {
        private bool _stackTapped;

        public static BindableProperty TappedCommandProperty =
                      BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(TappableStackLayout));

        public static BindableProperty TappedCommandParameterProperty =
                      BindableProperty.Create(nameof(TappedCommandParameter), typeof(object), typeof(TappableStackLayout));

        public static BindableProperty AnimateInnerImageOnTapProperty =
                      BindableProperty.Create(nameof(AnimateInnerImageOnTap), typeof(bool), typeof(TappableStackLayout), false);

        public static BindableProperty ShowTapEffectProperty =
                      BindableProperty.Create(nameof(ShowTapEffect), typeof(bool), typeof(TappableStackLayout), false);

        public static BindableProperty TapEffectColorProperty =
                      BindableProperty.Create(nameof(TapEffectColor), typeof(Color), typeof(TappableStackLayout), Color.FromHex("#D9D9D9"));

        public static BindableProperty TapEffectDurationMilisecondsProperty =
                      BindableProperty.Create(nameof(TapEffectDurationMiliseconds), typeof(int), typeof(TappableStackLayout), 50);

        public int TapEffectDurationMiliseconds
        {
            get { return (int)GetValue(TapEffectDurationMilisecondsProperty); }
            set { SetValue(TapEffectDurationMilisecondsProperty, value); }
        }

        public Color TapEffectColor
        {
            get { return (Color)GetValue(TapEffectColorProperty); }
            set { SetValue(TapEffectColorProperty, value); }
        }

        public bool ShowTapEffect
        {
            get { return (bool)GetValue(ShowTapEffectProperty); }
            set { SetValue(ShowTapEffectProperty, value); }
        }

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

        public bool AnimateInnerImageOnTap
        {
            get { return (bool)GetValue(AnimateInnerImageOnTapProperty); }
            set { SetValue(AnimateInnerImageOnTapProperty, value); }
        }

        public ICommand TransitionCommand { get; set; }

        public TappableStackLayout()
        {
            InitializeComponent();

            TransitionCommand = new RelayCommand(async () => await OnStackLayoutTapped());
        }

        private async Task OnStackLayoutTapped()
        {
            if (_stackTapped)
            {
                return;
            }

            _stackTapped = true;
            if (AnimateInnerImageOnTap)
            {
                await AnimateInnerImageAsync();
            }
            else if (ShowTapEffect)
            {
                await this.MimicTapEffect(TapEffectColor, TapEffectDurationMiliseconds);
            }
            TappedCommand?.Execute(TappedCommandParameter);
            _stackTapped = false;
        }

        private async Task AnimateInnerImageAsync()
        {
	       await this.SetAnimationWithScaleOffset(0.1, 120);
//            TappableImage tappableImage = Children.OfType<TappableImage>().FirstOrDefault(x => x.IsVisible);
//            if (tappableImage != null)
//            {
//                await tappableImage.AnimateImage();
//            }
        }
    }
}
