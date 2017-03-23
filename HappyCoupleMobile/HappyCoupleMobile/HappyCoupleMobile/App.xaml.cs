using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Enums;
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
            //await InitDatabase();
            await FullInitDatabase();

            await LogUser();

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

            SimpleIoc.Default.Register<INotificator<IShoppingListObserver, ShoppingList>, IShoppingListNotificator>();
            SimpleIoc.Default.Register<INotificator<IProductObserver, Product>, IProductObserver>();

            SimpleIoc.Default.Register<INotificationManager,NotificationManager>();

            SimpleIoc.Default.Register<ISqliteConnectionProvider, SqliteConnectionProvider>();
            SimpleIoc.Default.Register<IDatabaseInitializer, DatabaseInitializer>();

            SimpleIoc.Default.Register<ISimpleAuthService, SimpleAuthService>();
            SimpleIoc.Default.Register<INavigationPageService, NavigationPageService>();
            SimpleIoc.Default.Register<IProductServices, ProductService>();

            SimpleIoc.Default.Register<IShoppingListRepository, ShoppingListRepository>();
            SimpleIoc.Default.Register<IUserRepository, UserRepository>();

            SimpleIoc.Default.Register<IProductTypeDao, ProductTypeDao>();
            SimpleIoc.Default.Register<IShoppingListDao, ShoppingListDao>();
            SimpleIoc.Default.Register<IProductDao, ProductDao>();
            SimpleIoc.Default.Register<IUserDao, UserDao>();


            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<ShoppingsViewModel>(true);
            SimpleIoc.Default.Register<AddProductViewModel>(true);
            SimpleIoc.Default.Register<EditShoppingListViewModel>(true);
            SimpleIoc.Default.Register<FavouriteProductsViewModel>(true);
        }

        private async Task InitDatabase()
        {
            var databaseInitializer = SimpleIoc.Default.GetInstance<IDatabaseInitializer>();
            await databaseInitializer.EnsureAllTableExistsAsync();
        }

        private async Task FullInitDatabase()
        {
            var databaseInitializer = SimpleIoc.Default.GetInstance<IDatabaseInitializer>();
            await databaseInitializer.FullDatabaseInitializeAsync();

            await InsertProductTypes();
            await InsertMockedData();
        }

        private async Task InsertProductTypes()
        {
            IShoppingListRepository shoppingListRepository = SimpleIoc.Default.GetInstance<IShoppingListRepository>();

            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Owoce", "Fruits"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Oliwa", "Olive"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Sok", "Drink"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Marchewka", "Vege"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Rybka", "Fish"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Chleb", "Bread"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Mięso", "Meat"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Bazylia", "Spice"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Mleko", "Dairy"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Ryż", "Grain"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Food", "Food"));
            await shoppingListRepository.InsertProductTypeAsync(MockedData.GetProductType("Beer", "Beer"));


            var productTypes = await shoppingListRepository.GetAllProductTypesAsync();
        }

        private async Task InsertMockedData()
        {
            IShoppingListRepository shoppingListRepository = SimpleIoc.Default.GetInstance<IShoppingListRepository>();

            var productTypes = await shoppingListRepository.GetAllProductTypesAsync();

            await shoppingListRepository.InsertShoppingListAsync(
                MockedData.GetShoppingList("Lista Świąteczna", 1, string.Empty));
            await shoppingListRepository.InsertShoppingListAsync(
                MockedData.GetShoppingList("Cotygodniowe zakupy", 1, string.Empty));
            await shoppingListRepository.InsertShoppingListAsync(
                MockedData.GetShoppingList("Imprezka na weekend", 1, string.Empty));

            IList<ShoppingList> lists = await shoppingListRepository.GetAllShoppingListAsync();

            if (lists.Count == 3)
            {

                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        productTypes[3].Id, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                        (1, "Biedra", lists[2].Id,
                        productTypes[0].Id, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        productTypes[10].Id, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        productTypes[2].Id, 2));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        productTypes[4].Id, 4));

                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "Najlepiej to kupić w lidlu", lists[1].Id,
                       productTypes[8].Id, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                        (1, "Biedra", lists[1].Id, productTypes[3].Id, 1));

                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "chyba najtaniej będzie w Auchan", lists[0].Id,
                        productTypes[7].Id, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                        (1, "Biedra", lists[0].Id, productTypes[5].Id, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "Coś na sałatkę", lists[0].Id, productTypes[9].Id,
                        1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    (1, "Coś na sałatkę", lists[0].Id,
                        productTypes[10].Id, 1));
            }

            IList<ShoppingList> shoppingLists = await shoppingListRepository.GetAllShoppingListWithProductsAsync();
            var products = await shoppingListRepository.GetAllProductsWithChildrenAsync();
        }

        private async Task LogUser()
        {
            var databaseInitializer = SimpleIoc.Default.GetInstance<ISimpleAuthService>();

            await databaseInitializer.LogIn();
        }
    }
}
