
using System;
using System.ComponentModel;
using CoreGraphics;
using DACircularProgress;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(CircularProgress), typeof(CircularProgressRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
    public class CircularProgressRenderer : VisualElementRenderer<StackLayout>
    {
        private CircularProgressView progressView;

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            var rect = new CGRect(0,0, 50, 50);
            progressView = new CircularProgressView(rect);

            progressView.RoundedCorners = true;
            progressView.TrackTintColor = Color.FromHex("#F6585D").ToUIColor();
            progressView.ProgressTintColor = Color.FromHex("#30AD63").ToUIColor();
            progressView.SetProgress(0f, true);

            AddSubview(progressView);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var circularProgress = sender as CircularProgress;

            if (circularProgress == null)
            {
                return;
            }

            if (e.PropertyName == CircularProgress.PercentProperty.PropertyName)
            {
                progressView.SetProgress(PercentToProgressUnit(circularProgress.Percent), true);
            }
        }

        private nfloat PercentToProgressUnit(int circularProgressPercent)
        {
            nfloat procentFloat = (nfloat)circularProgressPercent / 100;
            return procentFloat;
        }
    }
}