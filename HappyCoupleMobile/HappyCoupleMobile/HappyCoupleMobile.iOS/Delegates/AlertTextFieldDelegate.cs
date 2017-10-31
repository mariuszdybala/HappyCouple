using Foundation;
using UIKit;

namespace HappyCoupleMobile.iOS.Delegates
{
	public class AlertTextFieldDelegate : UITextFieldDelegate
	{
		public override void EditingEnded(UITextField textField, UITextFieldDidEndEditingReason reason)
		{
		}

		public override void EditingStarted(UITextField textField)
		{
		}
		
		public override bool ShouldBeginEditing(UITextField textField)
		{
			return true;
		}

		public override bool ShouldEndEditing(UITextField textField)
		{
			return true;
		}
	}
}
