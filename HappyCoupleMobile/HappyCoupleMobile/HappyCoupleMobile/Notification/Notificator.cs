using System.Collections.Generic;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Notification.Interfaces;

namespace HappyCoupleMobile.Notification
{
    public class Notificator<TObserver,TData> : INotificator<TObserver, TData> where TObserver : IDataObserver<TData>
    {
        private readonly List<TObserver> _observers = new List<TObserver>();

        public void Attach(TObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(TObserver observer)
        {
            _observers.Add(observer);
        }

        public void Update(TData data)
        {
            foreach (var o in _observers)
            {
                o.Upadte(data);
            }
        }

        public void Remove(TData data)
        {
            foreach (var o in _observers)
            {
                o.Remove(data);
            }
        }

        public void Add(TData data)
        {
            foreach (var o in _observers)
            {
                o.Add(data);
            }
        }
    }
}