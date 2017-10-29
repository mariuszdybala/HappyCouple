using UIKit;

namespace HappyCoupleMobile.iOS.Delegates
{
	public class CustomTextFieldDelegate : UITextFieldDelegate
	{
		public override bool ShouldReturn(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}
	}
}
