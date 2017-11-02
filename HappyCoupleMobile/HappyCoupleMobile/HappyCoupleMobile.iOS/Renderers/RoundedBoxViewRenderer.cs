using System.ComponentModel;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
    public class RoundedBoxViewRenderer : ViewRenderer
    {
	    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if (Element != null)
            {
                Layer.MasksToBounds = true;
                UpdateCornerRadius(Element as RoundedBoxView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == RoundedBoxView.CornerRadiusProperty.PropertyName)
            {
                UpdateCornerRadius(Element as RoundedBoxView);
            }
        }

        void UpdateCornerRadius(RoundedBoxView box)
        {
            Layer.CornerRadius = (float)box.CornerRadius;

	        var stackLayout = Element as StackLayout;

	        if (stackLayout == null)
	        {
		        return;
	        }
	        
	        Layer.BackgroundColor = stackLayout.BackgroundColor.ToCGColor();
        }
    }
}