namespace HappyCoupleMobile.Notification.Interfaces
{
    public interface IDataObserver<in T>
    {
        void Upadte(T data);
        void Remove<TData>(TData data);
        void Add<TData>(TData data);
    }
}