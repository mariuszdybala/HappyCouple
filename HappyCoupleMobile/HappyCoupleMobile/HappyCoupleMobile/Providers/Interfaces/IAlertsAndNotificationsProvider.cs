using System;
using System.Collections.Generic;
using HappyCoupleMobile.Custom;
using Xamarin.Forms;

namespace HappyCoupleMobile.Providers.Interfaces
{
	public interface IAlertsAndNotificationsProvider
	{
		void ShowFailedToast(string failedText = "Coś poszło nie tak ;/");
		void ShowSuccessToast(string successText = "Gotowe!");
		void ShowAlertWithTextField(string message, string title, Keyboard keyboardType, Action<string> confirmed);
		void ShowAlertWithConfirmation(string message, string title,  Action<bool> confirmed);
		void ShowActionSheet(string message, string title, IList<ActionSheetItem> actionSheetItems);
	}
}
