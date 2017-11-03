using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Data.Interfaces;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Migrations;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Notification;
using HappyCoupleMobile.Notification.Interfaces;
using HappyCoupleMobile.Providers;
using HappyCoupleMobile.Providers.Interfaces;
using HappyCoupleMobile.Repositories;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace HappyCoupleMobile
{
    public partial class App : Application
    {
        private static readonly Lazy<ViewModelLocator> LazyLocator = new Lazy<ViewModelLocator>(() => new ViewModelLocator());
        public static ViewModelLocator Locator => LazyLocator.Value;

	    private IStartApplicationService StartApplicationService => SimpleIoc.Default.GetInstance<IStartApplicationService>();

        public App()
        {
#if !GORILLA
            FeedIoC();
#endif
            InitializeComponent();

            MainPage = new NavigationPage(new ShoppingsView());
#if !GORILLA
            if (SimpleIoc.Default.IsRegistered<INavigationPageService>())
            {
                SimpleIoc.Default.Register(() => new NavigationPageService(MainPage.Navigation));
            }
#endif
        }

        protected override async void OnStart()
        {
            //SetNotificationsObservers();

	        await StartApplicationService.InitApplicationAsync();

            var navigationPage = (NavigationPage)MainPage;

            var homeView = navigationPage.CurrentPage;

            var shoppingsViewController = homeView.BindingContext as ShoppingsViewModel;

            if (shoppingsViewController != null)
            {
                await shoppingsViewController.GetAllShoppingListsAndInitView();
            }
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void FeedIoC()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IShoppingListNotificator, ShoppingListNotificator>();
            SimpleIoc.Default.Register<IProductNotificator, ProductNotificator>();

            SimpleIoc.Default.Register<INotificationManager, NotificationManager>();

            SimpleIoc.Default.Register<ISqliteConnectionProvider, SqliteConnectionProvider>();
            SimpleIoc.Default.Register<IDatabaseInitializer, DatabaseInitializer>();

            SimpleIoc.Default.Register<ISimpleAuthService, SimpleAuthService>();
            SimpleIoc.Default.Register<INavigationPageService, NavigationPageService>();
            SimpleIoc.Default.Register<IProductServices, ProductService>();
            SimpleIoc.Default.Register<IShoppingListService, ShoppingListService>();
	        SimpleIoc.Default.Register<IStartApplicationService, StartApplicationService>();

            SimpleIoc.Default.Register<IShoppingListRepository, ShoppingListRepository>();
            SimpleIoc.Default.Register<IUserRepository, UserRepository>();

            SimpleIoc.Default.Register<IProductTypeDao, ProductTypeDao>();
            SimpleIoc.Default.Register<IShoppingListDao, ShoppingListDao>();
            SimpleIoc.Default.Register<IProductDao, ProductDao>();
            SimpleIoc.Default.Register<IUserDao, UserDao>();
	        SimpleIoc.Default.Register<IConfigurationDao, ConfigurationDao>();

	        SimpleIoc.Default.Register<IMigrator, Migrator>();

            SimpleIoc.Default.Register<ShoppingsViewModel>(true);
            SimpleIoc.Default.Register<AddProductViewModel>(true);
            SimpleIoc.Default.Register<EditShoppingListViewModel>(true);
            SimpleIoc.Default.Register<FavouriteProductsViewModel>(true);
	        SimpleIoc.Default.Register<FavouriteProductTypeViewModel>(true);
	        SimpleIoc.Default.Register<ClosedShoppingListViewModel>(true);
        }
    }
}
