using HappyCoupleMobile.Notification.Interfaces;

namespace HappyCoupleMobile.Notification
{
    public interface INotificator<in TObserver, in TData> where TObserver : IDataObserver<TData>
    {
        void Attach(TObserver observer);
        void Detach(TObserver observer);
        void Update(TData data);
        void Remove(TData data);
        void Add(TData data);
    }
}