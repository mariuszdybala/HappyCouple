using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Services;

namespace HappyCoupleMobile.ViewModel.Abstract
{
    public class BaseHappyViewModel : ViewModelBase
    {
        private INavigationPageService _navigationService;
        public User Admin => _simpleAuthService.Admin;

        private readonly ISimpleAuthService _simpleAuthService;

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
        }
    }
}