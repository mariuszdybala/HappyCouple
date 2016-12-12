using HappyCoupleMobile.Enums;

namespace HappyCoupleMobile.Mvvm.Messages.Interface
{
    public interface IAlertMessage
    {
        string Message { get; }
        AlertType AlertType { get; }
        object Data { get; }
    }
}