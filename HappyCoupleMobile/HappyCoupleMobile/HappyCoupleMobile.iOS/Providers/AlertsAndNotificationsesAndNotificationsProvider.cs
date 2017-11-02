using System;
using System.Linq;
using BigTed;
using Foundation;
using HappyCoupleMobile.iOS.Delegates;
using HappyCoupleMobile.Notification;
using HappyCoupleMobile.Providers.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace HappyCoupleMobile.iOS.Providers
{
	public class AlertsAndNotificationsesAndNotificationsProvider : IAlertsAndNotificationsProvider
	{
		private UIAlertAction _addAction;

		public AlertsAndNotificationsesAndNotificationsProvider()
		{
			BTProgressHUD.ForceiOS6LookAndFeel = true;
		}

		public void ShowFailedToast(string failedText = "Coś poszło nie tak ;/")
		{
			BTProgressHUD.ShowImage(UIImage.FromFile("closewindow.png"), failedText);
		}

		public void ShowSuccessToast(string successText = "Gotowe!")
		{
			BTProgressHUD.ForceiOS6LookAndFeel = true;
			BTProgressHUD.ShowImage(UIImage.FromFile("checked.png"), successText);
		}

		public void ShowAlertWithConfirmation(string message, string title, Action<bool> confirmed)
		{
			var alertContoller = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			var cancelAction = UIAlertAction.Create("Anuluj", UIAlertActionStyle.Cancel,
				(action) => { confirmed?.Invoke(false); });

			_addAction = UIAlertAction.Create("OK", UIAlertActionStyle.Destructive, (action) =>
			{
				confirmed?.Invoke(true);
			});

			alertContoller.AddAction(cancelAction);
			alertContoller.AddAction(_addAction);

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentModalViewController(alertContoller, true);
		}

		public void ShowAlertWithTextField(string message, string title, Keyboard keyboardType, Action<string> confirmed)
		{
			var alertContoller = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			alertContoller.AddTextField(textField =>
			{
				textField.KeyboardType = keyboardType == Keyboard.Numeric ? UIKeyboardType.NumberPad : UIKeyboardType.Default;
				textField.TextColor = UIColor.Blue;
				textField.EditingChanged += TextFieldOnValueChanged;
			});

			var cancelAction = UIAlertAction.Create("Anuluj", UIAlertActionStyle.Cancel,
				(action) => { });

			_addAction = UIAlertAction.Create("Dodaj", UIAlertActionStyle.Destructive, (action) =>
			{
				var nameTextField = alertContoller.TextFields.First();

				confirmed?.Invoke(nameTextField.Text);
			});

			_addAction.Enabled = false;

			alertContoller.AddAction(cancelAction);
			alertContoller.AddAction(_addAction);

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentModalViewController(alertContoller, true);
		}

		private void TextFieldOnValueChanged(object sender, EventArgs eventArgs)
		{
			var textField = (UITextField) sender;
			var text = textField.Text;

			textField.Placeholder = "Wpisz tekst";

			_addAction.Enabled = !string.IsNullOrWhiteSpace(text);
		}
	}
}
