namespace HappyCoupleMobile.Mvvm.Messages.Interface
{
    public interface IMessageData
    {
        void AddData(string key, object value);
        TValue GetValue<TValue>(string key) where TValue : new();
        TValue GetValue<TValue>() where TValue : new();
    }
}