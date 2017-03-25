using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Notification.Interfaces
{
    public interface INotificationManager
    {
        void RegisterObservers();
        void UpdateProduct(Product product);
        void RemoveProduct(Product product);
        void AddProduct(Product product);
        void UpdateShoppingList(ShoppingList shoppingList);
        void RemoveShoppingList(ShoppingList shoppingList);
        void AddShoppingList(ShoppingList shoppingList);
    }
}