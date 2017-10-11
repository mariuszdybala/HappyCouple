using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace HappyCoupleMobile.ViewModel
{
    public class ViewModelLocator
    {
#if !GORILLA           
        public ShoppingsViewModel ShoppingListViewModel => GetViewModel<ShoppingsViewModel>();
        public EditShoppingListViewModel EditShoppingListViewModel => GetViewModel<EditShoppingListViewModel>();
        public AddProductViewModel AddProductViewModel => GetViewModel<AddProductViewModel>();
        public FavouriteProductsViewModel FavouriteProductsViewModel => GetViewModel<FavouriteProductsViewModel>();
        public FavouriteProductTypeViewModel FavouriteProductTypeViewModel => GetViewModel<FavouriteProductTypeViewModel>();

        public static T GetViewModel<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public static void Cleanup()
        {
        }
#endif
    }
}