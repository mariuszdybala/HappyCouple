using HappyCoupleMobile.Model;
using HappyCoupleMobile.Notification.Interfaces;

namespace HappyCoupleMobile.Notification
{
    public class ProductNotificator: Notificator<IProductObserver, Product>, IProductNotificator
    {
        
    }
}