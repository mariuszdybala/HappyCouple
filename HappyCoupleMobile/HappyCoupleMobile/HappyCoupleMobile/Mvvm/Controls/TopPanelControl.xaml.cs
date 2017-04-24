using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Helpers;
using HappyCoupleMobile.Mvvm.Renderers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class TopPanelControl : StackLayout
    {
        public static readonly BindableProperty LeftIconSourceProperty = BindableProperty.Create(
        nameof(LeftIconSource), typeof(FileImageSource), typeof(TopPanelControl), (FileImageSource)Application.Current.Resources["Couple"]);

        public static readonly BindableProperty RightIconSourceProperty = BindableProperty.Create(
        nameof(RightIconSource), typeof(FileImageSource), typeof(TopPanelControl), null);

        public static readonly BindableProperty PanelHeaderProperty = BindableProperty.Create(
        nameof(PanelHeader), typeof(string), typeof(TopPanelControl));

        public static readonly BindableProperty RightIconTapCommandProperty = BindableProperty.Create(
        nameof(RightIconTapCommand), typeof(ICommand), typeof(TopPanelControl), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty LeftIconTapCommandProperty = BindableProperty.Create(
        nameof(LeftIconTapCommand), typeof(ICommand), typeof(TopPanelControl), defaultBindingMode: BindingMode.OneWay);

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

        public string PanelHeader
        {
            get { return (string)GetValue(PanelHeaderProperty); }
            set { SetValue(PanelHeaderProperty, value); }
        }

        public ICommand RightIconTapCommand
        {
            get { return (ICommand)GetValue(RightIconTapCommandProperty); }
            set { SetValue(RightIconTapCommandProperty, value); }
        }

        public ICommand LeftIconTapCommand
        {
            get { return (ICommand)GetValue(LeftIconTapCommandProperty); }
            set { SetValue(LeftIconTapCommandProperty, value); }
        }

        public TopPanelControl()
        {
            InitializeComponent();
        }

        private void OnLeftIconImageTapped(object sender, EventArgs e)
        {
            Image leftIconImage = (Image) sender;
            leftIconImage.SetAnimation();

            if (LeftIconTapCommand != null && LeftIconTapCommand.CanExecute(null))
            {
                LeftIconTapCommand.Execute(null);
            }
        }

        private void OnRightIconImageTapped(object sender, EventArgs e)
        {
            Image leftIconImage = (Image)sender;
            leftIconImage.SetAnimation();

            if (RightIconTapCommand != null && RightIconTapCommand.CanExecute(null))
            {
                RightIconTapCommand.Execute(null);
            }
        }
    }
}
