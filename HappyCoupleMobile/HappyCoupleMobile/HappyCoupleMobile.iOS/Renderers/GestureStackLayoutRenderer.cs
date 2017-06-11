using System;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GestureStackLayout), typeof(GestureStackLayoutRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
    public class GestureStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
		private UISwipeGestureRecognizer _swipeLeft;
		private UISwipeGestureRecognizer _swipeRight;
		private UITapGestureRecognizer _tapGesture;

		protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
		{
			base.OnElementChanged(e);

			_tapGesture = new UITapGestureRecognizer(CallEventOnTap);

			_swipeLeft = new UISwipeGestureRecognizer(
				() =>
				{
					CallEventOnGesture(SwipeDirection.Left);
				}
			)
			{
				Direction = UISwipeGestureRecognizerDirection.Left,
			};

			_swipeRight = new UISwipeGestureRecognizer(
				() =>
				{
					CallEventOnGesture(SwipeDirection.Right);
				}
			)
			{
				Direction = UISwipeGestureRecognizerDirection.Right,
			};

			if (e.NewElement == null)
			{
				if (_swipeLeft != null)
				{
					RemoveGestureRecognizer(_swipeLeft);
				}
				if (_swipeRight != null)
				{
					RemoveGestureRecognizer(_swipeRight);
				}
				if (_tapGesture != null)
				{
					RemoveGestureRecognizer(_tapGesture);
				}
			}

			if (e.OldElement == null)
			{
				AddGestureRecognizer(_swipeRight);
				AddGestureRecognizer(_swipeLeft);
				AddGestureRecognizer(_tapGesture);
			}
		}

		private void CallEventOnTap()
		{
			GestureStackLayout qestureStackLayout = (GestureStackLayout)Element;

			qestureStackLayout.OnTap();
		}

		private void CallEventOnGesture(SwipeDirection swipeDirection)
		{
			GestureStackLayout qestureStackLayout = (GestureStackLayout)Element;

			if (swipeDirection == SwipeDirection.Left)
			{
				qestureStackLayout.OnSwipeLeft();
			}
			else
			{
				qestureStackLayout.OnSwipeRight();
			}
		}
    }
}
