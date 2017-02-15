using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Services;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel.Abstract
{
    public abstract class BaseHappyViewModel : ViewModelBase
    {
        private INavigationPageService _navigationService;
        private readonly ISimpleAuthService _simpleAuthService;

        public User Admin => _simpleAuthService.Admin;
        public ContentPage Page { get; set; }

        public EventHandler ViewAppeared { get; set; }

        public Command GoBackCommand { get; set; }

        public INavigationPageService NavigationService
        {
            get
            {
                return _navigationService ?? (_navigationService = SimpleIoc.Default.GetInstance<NavigationPageService>());
            }
            set { _navigationService = value; }
        }

        protected BaseHappyViewModel(ISimpleAuthService simpleAuthService)
        {
            _simpleAuthService = simpleAuthService;
            RegisterBaseCommand();

            ViewAppeared += OnViewLoaded;
        }

        private void RegisterBaseCommand()
        {
            GoBackCommand = new Command(async () => await OnGoBackCommand());
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

        private async Task OnGoBackCommand()
        {
            CleanResources();
            await NavigateBack();
        }

        protected virtual void CleanResources()
        {
            
        }
    }
}