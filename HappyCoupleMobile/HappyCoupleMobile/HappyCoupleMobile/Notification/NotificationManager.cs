using HappyCoupleMobile.Notification.Interfaces;

namespace HappyCoupleMobile.Notification
{
    public class NotificationManager : INotificationManager
    {
        private readonly IProductNotificator _productNotificator;
        private readonly IShoppingListNotificator _shoppingListNotificator;


        public NotificationManager(IProductNotificator productNotificator, IShoppingListNotificator shoppingListNotificator)
        {
            _productNotificator = productNotificator;
            _shoppingListNotificator = shoppingListNotificator;
        }

        public void RegisterObservers()
        {
        }

        private void RegisterShoppingListObservers()
        {
            
        }

        private void RegisterProductObservers()
        {
            
        }
    }
}