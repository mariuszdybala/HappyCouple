using System;
using Android.Views;

namespace HappyCoupleMobile.Droid.Custom
{
    public class CustomGestureListener : GestureDetector.SimpleOnGestureListener
    {
        private static int _swipeThreshold = 100;
        private static int _swipeVelocityThreshold = 100;

        public event EventHandler OnSwipeLeft;
        public event EventHandler OnTap;
        public event EventHandler OnSwipeRight;

        public override bool OnSingleTapUp(MotionEvent e)
        {
            OnTap?.Invoke(this, null);

            return base.OnSingleTapUp(e);
        }

        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            float diffY = e2.GetY() - e1.GetY();
            float diffX = e2.GetX() - e1.GetX();

            if (!(Math.Abs(diffX) > Math.Abs(diffY)))
            {
                return base.OnFling(e1, e2, velocityX, velocityY);
            }

            if (Math.Abs(diffX) > _swipeThreshold && Math.Abs(velocityX) > _swipeVelocityThreshold)
            {
                if (diffX > 0)
                {
                    OnSwipeRight?.Invoke(this, null);
                }
                else
                {
                    OnSwipeLeft?.Invoke(this, null);
                }
            }

            return base.OnFling(e1, e2, velocityX, velocityY);
        }
    }
}
