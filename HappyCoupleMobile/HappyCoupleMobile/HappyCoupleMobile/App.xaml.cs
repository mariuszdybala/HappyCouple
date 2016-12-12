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
using HappyCoupleMobile.Providers;
using HappyCoupleMobile.Providers.Interfaces;
using HappyCoupleMobile.Repositories;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services;
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
            FeedIoC();
            InitializeComponent();

            MainPage = new ShoppingListView();

            if (SimpleIoc.Default.IsRegistered<INavigationPageService>())
            {
                SimpleIoc.Default.Register(() => new NavigationPageService(MainPage.Navigation));
            }
        }

        protected override async void OnStart()
        {
            await InitDatabase();
            await LogUser();
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

            SimpleIoc.Default.Register<ISqliteConnectionProvider, SqliteConnectionProvider>();
            SimpleIoc.Default.Register<IDatabaseInitializer, DatabaseInitializer>();


            SimpleIoc.Default.Register<ISimpleAuthService, SimpleAuthService>();


            SimpleIoc.Default.Register<IShoppingListRepository, ShoppingListRepository>();
            SimpleIoc.Default.Register<IUserRepository, UserRepository>();


            SimpleIoc.Default.Register<IShoppingListDao,ShoppingListDao>();
            SimpleIoc.Default.Register<IProductDao,ProductDao>();
            SimpleIoc.Default.Register<IUserDao,UserDao>();


            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<ShoppingListViewModel>(true);
        }

        private async Task InitDatabase()
        {
            var databaseInitializer = SimpleIoc.Default.GetInstance<IDatabaseInitializer>();
            await databaseInitializer.EnsureAllTableExistsAsync();

            //await databaseInitializer.FullDatabaseInitializeAsync();
            // await InsertMockedData();
        }

        private async Task InsertMockedData()
        {
            IShoppingListRepository shoppingListRepository = SimpleIoc.Default.GetInstance<IShoppingListRepository>();

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
                    ("Krewetki", 1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        ProductType.Fish, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                        ("Bułki", 1, "Biedra", lists[2].Id, ProductType.Bread, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    ("Wino", 1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        ProductType.Alcohol, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    ("Chipsy", 1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        ProductType.Other, 2));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    ("Soki", 1, "Najlepiej to kupić w lidlu", lists[2].Id,
                        ProductType.Drink, 4));

                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    ("Łosoś", 1, "Najlepiej to kupić w lidlu", lists[1].Id,
                        ProductType.Fish, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                        ("Bułki", 1, "Biedra", lists[1].Id, ProductType.Bread, 1));

                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    ("Napoje", 1, "chyba najtaniej będzie w Auchan", lists[0].Id,
                        ProductType.Drink, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                        ("Wódeczka", 1, "Biedra", lists[0].Id, ProductType.Alcohol, 1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    ("Sałata", 1, "Coś na sałatkę", lists[0].Id, ProductType.Vege,
                        1));
                await shoppingListRepository
                    .InsertProductAsync(MockedData.GetProduct
                    ("Pomidory", 1, "Coś na sałatkę", lists[0].Id,
                        ProductType.Vege, 1));
            }
        }

        private async Task LogUser()
        {
            var databaseInitializer = SimpleIoc.Default.GetInstance<ISimpleAuthService>();

            await databaseInitializer.LogIn();
        }
    }
}
