
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
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            base.OnElementChanged(e);

            var rect = new CGRect(0,0, 50, 50);
            var progressView = new CircularProgressView(rect);

            progressView.RoundedCorners = true;
            progressView.TrackTintColor = Color.FromHex("#F6585D").ToUIColor();
            progressView.ProgressTintColor = Color.FromHex("#30AD63").ToUIColor();
            progressView.SetProgress(0.5f, true);

            AddSubview(progressView);
        }
    }
}