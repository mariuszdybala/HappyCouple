using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers
{
	public class SwipableViewCell : ViewCell
	{
        public static BindableProperty CommentProperty =
            BindableProperty.Create(nameof(Comment), typeof(string), typeof(SwipableViewCell), defaultValue: null);
        public static BindableProperty ProductNameProperty =
            BindableProperty.Create(nameof(ProductName), typeof(string), typeof(SwipableViewCell), defaultValue: null);
        public static BindableProperty QuentityProperty =
            BindableProperty.Create(nameof(Quentity), typeof(int), typeof(SwipableViewCell), defaultValue:0);

        public int Quentity
        {
            get => (int)GetValue(QuentityProperty);
            set => SetValue(QuentityProperty, value);
        }

        public string ProductName
        {
            get => (string)GetValue(ProductNameProperty);
            set => SetValue(ProductNameProperty, value);
        }

        public string Comment
        {
            get => (string)GetValue(CommentProperty);
            set => SetValue(CommentProperty, value);
        }
	}
}
