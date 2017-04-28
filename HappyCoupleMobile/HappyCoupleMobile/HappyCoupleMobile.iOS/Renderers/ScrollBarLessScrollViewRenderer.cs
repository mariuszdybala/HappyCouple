
using System.ComponentModel;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportCell(typeof(ScrollBarLessScrollView), typeof(ScrollBarLessScrollViewRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
    public class ScrollBarLessScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            this.ShowsHorizontalScrollIndicator = false;
            this.ShowsVerticalScrollIndicator = false;

        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element != null)
            {
                this.ShowsHorizontalScrollIndicator = false;
                this.ShowsVerticalScrollIndicator = false;
            }
        }
    }
}