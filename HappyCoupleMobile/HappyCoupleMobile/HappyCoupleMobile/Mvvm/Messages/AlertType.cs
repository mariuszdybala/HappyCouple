using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Mvvm.Messages.Interface;

namespace HappyCoupleMobile.Mvvm.Messages
{
    public class AlertMessage : IAlertMessage
    {
        public string Message { get; }

        public AlertType AlertType { get; }

        public object Data { get; }

        public AlertMessage(string message) : this(message, AlertType.Information)
        {
        }

        public AlertMessage(string message, AlertType alertType) : this(message, alertType, null)
        {
        }

        public AlertMessage(string message, AlertType alertType, object data)
        {
            Message = message;
            AlertType = alertType;
            Data = data;
        }
    }
}