using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappyCoupleMobile.Helpers
{
    public static class Animation
    {
        public static async void SetAnimation(this VisualElement element)
        {
            element.AnchorX = 0.48;
            element.AnchorX = 0.48;
            await element.ScaleTo(0.8, 50, Easing.Linear);
            await Task.Delay(75);
            await element.ScaleTo(1, 50, Easing.Linear);
        }
    }
}
