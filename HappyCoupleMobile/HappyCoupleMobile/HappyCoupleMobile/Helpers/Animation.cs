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

        public static async Task SetAnimation(this VisualElement element, double scale, uint velocity)
        {
            element.AnchorX = 0.48;
            element.AnchorX = 0.48;
            await element.ScaleTo(scale, velocity, Easing.Linear);
            await Task.Delay(75);
            await element.ScaleTo(1, velocity, Easing.Linear);
        }

        public static async Task SetLoopAnimation(this VisualElement element, double scale, uint velocity)
        {
            element.AnchorX = 0.48;
            element.AnchorX = 0.48;

            while (true)
            {
                await element.ScaleTo(scale, velocity, Easing.Linear);
                await Task.Delay(75);
                await element.ScaleTo(1, velocity, Easing.Linear);
            }
        }

        public static async Task MimicTapEffect(this Layout layout, Color? effectColor = null, int effectDurationMiliseconds = 50)
        {
            Color colorBeforeChange = layout.BackgroundColor;
            layout.BackgroundColor = effectColor ?? Color.FromHex("#D9D9D9");
            await Task.Delay(effectDurationMiliseconds);
            layout.BackgroundColor = colorBeforeChange;
        }
    }
}
