using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Mvvm.Messages.Interface;

namespace HappyCoupleMobile.Mvvm.Messages
{
    public class FeedbackMessage : MessageData, IFeedbackMessage
    {
	    public OperationMode OperationMode { get; set; }
	    
        public FeedbackMessage()
        {
            
        }

        public FeedbackMessage(string key, object value) : base(key, value)
        {
            
        }
	    
	    public FeedbackMessage(string key, object value, OperationMode operationMode) : base(key, value)
	    {
		    OperationMode = operationMode;
	    }
    }
}