using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class TopPanelControl : ContentView
    {
        public static readonly BindableProperty LeftIconSourceProperty = BindableProperty.Create(
        nameof(LeftIconSource), typeof(FileImageSource), typeof(TopPanelControl), Application.Current.Resources["Couple"]);

        public static readonly BindableProperty RightIconSourceProperty = BindableProperty.Create(
        nameof(RightIconSource), typeof(FileImageSource), typeof(TopPanelControl), string.Empty);

        public FileImageSource LeftIconSource
        {
            get { return (FileImageSource)GetValue(LeftIconSourceProperty); }
            set { SetValue(LeftIconSourceProperty, value); }
        }

        public FileImageSource RightIconSource
        {
            get { return (FileImageSource)GetValue(RightIconSourceProperty); }
            set { SetValue(RightIconSourceProperty, value); }
        }

        public TopPanelControl()
        {
            InitializeComponent();
        }
    }
}
