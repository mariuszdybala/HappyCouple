using System;
using UIKit;
using Xamarin.Forms;

namespace HappyCoupleMobile.iOS.Custom
{
    public static class Helpers
    {
        public static double Clamp(this double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }

        public static UIView FindFirstResponder(this UIView view)
        {
            if (view.IsFirstResponder)
            {
                return view;
            }

            foreach (var subView in view.Subviews)
            {
                var firstResponder = subView.FindFirstResponder();
                if (firstResponder != null)
                {
                    return firstResponder;
                }
            }

            return null;
        }

        public static void RegisterEffectControlProvider(IEffectControlProvider self, IElementController oldElement, IElementController newElement)
        {
            IElementController controller = oldElement;
            if (controller != null && controller.EffectControlProvider == self)
            {
                controller.EffectControlProvider = null;
            }

            controller = newElement;
            if (controller != null)
            {
                controller.EffectControlProvider = self;
            }
        }
    }
}