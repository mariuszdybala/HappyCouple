using HappyCoupleMobile.Enums;

namespace HappyCoupleMobile.Mvvm.Messages.Interface
{
    public interface IFeedbackMessage : IMessageData
    {
	    OperationMode OperationMode { get; set; }
    }
}