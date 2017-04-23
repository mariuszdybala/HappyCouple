using HappyCoupleMobile.Mvvm.Messages.Interface;

namespace HappyCoupleMobile.Mvvm.Messages
{
    public class FeedbackMessage : MessageData, IFeedbackMessage
    {
        public FeedbackMessage()
        {
            
        }

        public FeedbackMessage(string key, object value) : base(key, value)
        {
            
        }
    }
}