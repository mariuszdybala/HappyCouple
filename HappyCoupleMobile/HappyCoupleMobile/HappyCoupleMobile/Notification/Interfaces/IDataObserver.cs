namespace HappyCoupleMobile.Notification.Interfaces
{
    public interface IDataObserver<in T>
    {
        void Upadte(T data);
        void Remove(T data);
        void Add(T data);
    }
}