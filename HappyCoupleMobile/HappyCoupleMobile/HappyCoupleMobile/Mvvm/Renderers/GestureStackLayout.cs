using System;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Renderers
{
    public class GestureStackLayout : StackLayout
    {
		public event Action SwipeLeft;
		public event Action SwipeRight;
		public event Action Tap;

		public virtual void OnSwipeLeft()
		{
			SwipeLeft?.Invoke();
		}

		public virtual void OnSwipeRight()
		{
			SwipeRight?.Invoke();
		}

		public virtual void OnTap()
		{
			Tap?.Invoke();
		}
    }
}
