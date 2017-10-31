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
        nameof(LeftIconSource), typeof(FileImageSource), typeof(TopPanelControl), null);
	    
	    public static readonly BindableProperty LeftLabelTextProperty = BindableProperty.Create(
		    nameof(LeftLabelText), typeof(string), typeof(TopPanelControl));

        public static readonly BindableProperty RightIconSourceProperty = BindableProperty.Create(
        nameof(RightIconSource), typeof(FileImageSource), typeof(TopPanelControl), null);

        public static readonly BindableProperty PanelHeaderProperty = BindableProperty.Create(
        nameof(PanelHeader), typeof(string), typeof(TopPanelControl));

        public static readonly BindableProperty RightIconTapCommandProperty = BindableProperty.Create(
        nameof(RightIconTapCommand), typeof(ICommand), typeof(TopPanelControl), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty LeftItemTapCommandProperty = BindableProperty.Create(
        nameof(LeftItemTapCommand), typeof(ICommand), typeof(TopPanelControl), defaultBindingMode: BindingMode.OneWay);

        public FileImageSource LeftIconSource
        {
            get { return (FileImageSource)GetValue(LeftIconSourceProperty); }
            set { SetValue(LeftIconSourceProperty, value); }
        }
	    
	    public string LeftLabelText
	    {
		    get { return (string)GetValue(LeftLabelTextProperty); }
		    set { SetValue(LeftLabelTextProperty, value); }
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

        public ICommand LeftItemTapCommand
        {
            get { return (ICommand)GetValue(LeftItemTapCommandProperty); }
            set { SetValue(LeftItemTapCommandProperty, value); }
        }

        public TopPanelControl()
        {
            InitializeComponent();
        }

        private void OnLeftItemTapped(object sender, EventArgs e)
        {
	        Xamarin.Forms.View leftView = (Xamarin.Forms.View) sender;
	        leftView.SetAnimation();

            if (LeftItemTapCommand != null && LeftItemTapCommand.CanExecute(null))
            {
                LeftItemTapCommand.Execute(null);
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
