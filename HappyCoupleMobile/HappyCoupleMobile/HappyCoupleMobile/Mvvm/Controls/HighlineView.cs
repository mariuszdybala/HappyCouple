using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public class HighlineView : BoxView
    {
        public HighlineView()
        {
            HeightRequest = 10;
            BackgroundColor = Color.Pink;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
