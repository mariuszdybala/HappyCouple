using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircularView), typeof(CircularViewRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{ 
	public class CircularViewRenderer : ViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged(e);

			Control.Layer.CornerRadius = 40;
		}
	}
}
