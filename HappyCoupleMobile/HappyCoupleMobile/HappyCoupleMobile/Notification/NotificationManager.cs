using HappyCoupleMobile.Model;
using HappyCoupleMobile.Notification.Interfaces;
using HappyCoupleMobile.ViewModel;

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
            RegisterShoppingListObservers();
            RegisterProductObservers();
        }

        public void UpdateProduct(Product product)
        {
            _productNotificator.Update(product);
        }

        public void RemoveProduct(Product product)
        {
            _productNotificator.Remove(product);
        }

        public void AddProduct(Product product)
        {
            _productNotificator.Add(product);
        }

        public void UpdateShoppingList(ShoppingList shoppingList)
        {
            _shoppingListNotificator.Update(shoppingList);
        }

        public void RemoveShoppingList(ShoppingList shoppingList)
        {
            _shoppingListNotificator.Remove(shoppingList);
        }

        public void AddShoppingList(ShoppingList shoppingList)
        {
            _shoppingListNotificator.Add(shoppingList);
        }
        private void RegisterProductObservers()
        {
#if !GORILLA
            _productNotificator.Attach(ViewModelLocator.GetViewModel<ShoppingsViewModel>());
            _productNotificator.Attach(ViewModelLocator.GetViewModel<EditShoppingListViewModel>());
#endif
        }

        private void RegisterShoppingListObservers()
        {
#if !GORILLA
            _shoppingListNotificator.Attach(ViewModelLocator.GetViewModel<ShoppingsViewModel>());
#endif
        }
    }
}