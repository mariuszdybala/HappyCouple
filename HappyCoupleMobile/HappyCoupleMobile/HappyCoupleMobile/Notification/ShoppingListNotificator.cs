using HappyCoupleMobile.Model;
using HappyCoupleMobile.Notification.Interfaces;

namespace HappyCoupleMobile.Notification
{
    public class ShoppingListNotificator:Notificator<IShoppingListObserver, ShoppingList>, IShoppingListNotificator
    {
        
    }
}