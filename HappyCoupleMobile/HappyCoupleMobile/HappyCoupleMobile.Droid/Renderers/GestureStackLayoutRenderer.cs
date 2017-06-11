using System;
using HappyCoupleMobile.Droid.Custom;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HappyCoupleMobile.Mvvm.Renderers;
using HappyCoupleMobile.Droid.Renderers;
using Android.Views;
using Android.Support.V4.View;
using Android.Util;

[assembly: ExportRenderer(typeof(GestureStackLayout), typeof(GestureStackLayoutRenderer))]
namespace HappyCoupleMobile.Droid.Renderers
{
    public class GestureStackLayoutRenderer : ViewRenderer
    {
		private GestureDetectorCompat _gestureRecognizer;
		private readonly InternalGestureDetector _tapDetector;
		private DisplayMetrics _displayMetrics;

		private float _startX;
		private float _startY;

		public GestureStackLayoutRenderer()
		{
			_tapDetector = new InternalGestureDetector();

			_tapDetector.OnTap += HandleTap;
			_tapDetector.OnSwipeLeft += HandleOnSwipeLeft;
			_tapDetector.OnSwipeRight += HandleOnSwipeRight;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
			{
				DetachedGestureListener();
			}

			if (e.OldElement == null)
			{
				AttachedGestureListener();
			}
		}

		private void HandleTap(MotionEvent motionEventArgs)
		{
			GestureStackLayout gestureStackLayout = (GestureStackLayout)Element;
			gestureStackLayout.OnTap();
		}

		private void HandleOnSwipeLeft(MotionEvent motionEventArgs)
		{
			GestureStackLayout gestureStackLayout = (GestureStackLayout)Element;
			gestureStackLayout.OnSwipeLeft();
		}

		private void HandleOnSwipeRight(MotionEvent motionEventArgs)
		{
			GestureStackLayout gestureStackLayout = (GestureStackLayout)Element;
			gestureStackLayout.OnSwipeRight();
		}

		private void AttachedGestureListener()
		{
			var context = Context;
			_displayMetrics = context.Resources.DisplayMetrics;
			_tapDetector.Density = _displayMetrics.Density;

			if (_gestureRecognizer == null)
			{
				_gestureRecognizer = new GestureDetectorCompat(context, _tapDetector);
			}

			Touch += ControlOnTouch;
		}

		private void DetachedGestureListener()
		{
			Touch -= ControlOnTouch;
		}

		private void ControlOnTouch(object sender, TouchEventArgs touchEventArgs)
		{
			_gestureRecognizer?.OnTouchEvent(touchEventArgs.Event);
		}

		public override bool DispatchTouchEvent(MotionEvent e)
		{
			Parent.RequestDisallowInterceptTouchEvent(false);

			switch (e.Action)
			{
				case MotionEventActions.Down:
					_startX = e.RawX;
					_startY = e.RawY;
					break;
				case MotionEventActions.Move:
					if (Math.Abs(_startX - e.RawX) > Math.Abs(_startY - e.RawY))
					{
						Parent.RequestDisallowInterceptTouchEvent(true);
					}
					break;
			}

			return base.DispatchTouchEvent(e);
		}

		private sealed class InternalGestureDetector : GestureDetector.SimpleOnGestureListener
		{
			private const int SwipeThresholdInPoints = 40;

			public Action<MotionEvent> OnSwipeLeft;
			public Action<MotionEvent> OnTap;
			public Action<MotionEvent> OnSwipeRight;

			public float Density { get; set; }

			public override bool OnSingleTapUp(MotionEvent e)
			{
				OnTap?.Invoke(e);
				return true;
			}

			public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
			{
				var dx = e2.RawX - e1.RawX;

				if (Math.Abs(dx) > SwipeThresholdInPoints * Density)
				{
					if (dx > 0)
					{
						OnSwipeRight?.Invoke(e2);
					}
					else
					{
						OnSwipeLeft?.Invoke(e2);
					}
				}

				return true;
			}
		}
    }
}
