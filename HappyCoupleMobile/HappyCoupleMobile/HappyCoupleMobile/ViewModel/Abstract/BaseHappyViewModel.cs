using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Services;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel.Abstract
{
    public class BaseHappyViewModel : ViewModelBase
    {
        private INavigationPageService _navigationService;
        private readonly ISimpleAuthService _simpleAuthService;

        public User Admin => _simpleAuthService.Admin;
        public ContentPage Page { get; set; }

        public EventHandler ViewAppeared { get; set; }

        public INavigationPageService NavigationService
        {
            get
            {
                return _navigationService ?? (_navigationService = SimpleIoc.Default.GetInstance<NavigationPageService>());
            }
            set { _navigationService = value; }
        }

        public BaseHappyViewModel(ISimpleAuthService simpleAuthService)
        {
            _simpleAuthService = simpleAuthService;

            ViewAppeared += OnViewLoaded;
        }

        public async Task NavigateTo<T>() where T: ContentPage , new()
        {
            await NavigationService.PushAsync<T>();
        }

        public async Task NavigateBack()
        {
            await NavigationService.PopAsync();
        }

        protected virtual void OnViewLoaded(object sender, EventArgs eventArgs)
        {
            Page = sender as ContentPage;
            if (Page != null)
            {
                RaisePropertyChanged(nameof(Page));
            }
        }
    }
}