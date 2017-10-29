using System;
using HappyCoupleMobile.Enums;
using Xamarin.Forms;

namespace HappyCoupleMobile.Custom
{
	public class SwipeButton
	{
		public string Text { get; set; }
		public SwipeButtonType ButtonType { get; set; }
		public Color Color { get; set; }
		public FileImageSource ImageSource { get; set; }
		public Action Clicked;
	}
}
