using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HappyCoupleMobile.View;
using Xamarin.Forms;

namespace HappyCoupleMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ToDoListView();
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

        private void InitDatabase()
        {
            
        }
    }
}
