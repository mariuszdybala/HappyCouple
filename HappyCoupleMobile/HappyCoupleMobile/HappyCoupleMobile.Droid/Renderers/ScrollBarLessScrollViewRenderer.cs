using System.ComponentModel;
using HappyCoupleMobile.Droid.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportCell(typeof(ScrollBarLessScrollView), typeof(ScrollBarLessScrollViewRenderer))]
namespace HappyCoupleMobile.Droid.Renderers
{
    public class ScrollBarLessScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;

        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HorizontalScrollBarEnabled = false;
            VerticalScrollBarEnabled = false;
        }

    }
}