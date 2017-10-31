using System;
using Xamarin.Forms;

namespace HappyCoupleMobile.Providers.Interfaces
{
	public interface IAlertsAndNotificationsProvider
	{
		void ShowFailedToast(string failedText = "Coś poszło nie tak ;/");
		void ShowSuccessToast(string successText = "Gotowe!");
		void ShowAlertWithTextField(string message, string title, Keyboard keyboardType, Action<string> confirmed);
	}
}
