using System;
using HappyCoupleMobile.iOS.Delegates;
using HappyCoupleMobile.Providers.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace HappyCoupleMobile.iOS.Providers
{
	public class AlertProvider : IAlertProvider
	{
		public void ShowAlertWithTextField(string message, string title)
		{
			var alertView = new UIAlertView();

			alertView.Title = title;
			alertView.Message = message;

			alertView.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			alertView.Clicked += AlertViewOnClicked;
			alertView.BackgroundColor = UIColor.Blue;
	        
			alertView.AddButton("OK");
			alertView.AddButton("Anuluj");

			var alertTextField = alertView.GetTextField(0);
			alertTextField.KeyboardType = UIKeyboardType.NumberPad;
			alertTextField.Delegate = new CustomTextFieldDelegate();
	        
			alertView.Show();
		}
		
		private void AlertViewOnClicked(object sender, UIButtonEventArgs uiButtonEventArgs)
		{
			var button = (UIAlertView) sender;
			var text = button.GetTextField(0).Text;
			
			Confirmed?.Invoke(text);	
		}

		public event Action<object> Confirmed;
	}
}
