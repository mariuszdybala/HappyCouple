using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappyCoupleMobile.Services
{
    public class NavigationPageService : INavigationPageService
    {
        private readonly INavigation _navigation;

        public NavigationPageService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task PushAsync<T>() where T : ContentPage, new()
        {
            ContentPage pushedPage = new T();

            if (_navigation.NavigationStack.Any())
            {
                var lastPage = _navigation.NavigationStack.Last() as ContentPage;

                if (lastPage?.GetType() == pushedPage.GetType())
                {
                    return;
                }
            }

            await _navigation.PushAsync(pushedPage);
        }

        public async Task PopAsync()
        {
            await _navigation.PopAsync(true);
        }

        public async Task GoToRoot()
        {
            await _navigation.PopToRootAsync();
        }

    }
}