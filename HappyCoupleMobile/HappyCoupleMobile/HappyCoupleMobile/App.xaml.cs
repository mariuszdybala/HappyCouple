using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using HappyCoupleMobile.Data;
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
           // await databaseInitializer.EnsureAllTableExistsAsync();

            await databaseInitializer.FullDatabaseInitializeAsync();
        }
    }
}
