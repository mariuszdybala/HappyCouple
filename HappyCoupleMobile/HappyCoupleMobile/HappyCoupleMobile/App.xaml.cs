using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
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

            MainPage = new ToDoListView();

            if (SimpleIoc.Default.IsRegistered<INavigationPageService>())
            {
                SimpleIoc.Default.Register(() => new NavigationPageService(MainPage.Navigation));
            }
        }

        protected override void OnStart()
        {
            InitDatabase();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private void FeedIoC()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<ToDoListViewModel>(true);
        }

        private void InitDatabase()
        {
            
        }
    }
}
