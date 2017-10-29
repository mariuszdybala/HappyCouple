using System;
using Xamarin.Forms;

namespace HappyCoupleMobile.Providers.Interfaces
{
	public interface IAlertProvider
	{
		 void ShowAlertWithTextField(string message, string title);
		 event Action<object> Confirmed;
	}
}
