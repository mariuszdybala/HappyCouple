using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.ViewModel.Abstract;
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
            ContentPage target = new T();
            if (_navigation.NavigationStack.Count != 0)
            {
                var lastPage = _navigation.NavigationStack.Last() as ContentPage;
                if (lastPage?.GetType() == target.GetType())
                {
                    return;
                }
            }
            await _navigation.PushAsync(target);
        }

        public async Task PopAsync()
        {
            await _navigation.PopAsync(true);
        }

        public async Task GoToRoot()
        {
            await _navigation.PopToRootAsync();
        }

	    public BaseHappyViewModel GetLastViewModel()
	    {
		    var previousContentPage = GetPreviousContentPage();

		    return previousContentPage?.BindingContext as BaseHappyViewModel;
	    }

	    private ContentPage GetPreviousContentPage()
	    {
		    if (_navigation.NavigationStack.Count > 1)
		    {
			    var index = _navigation.NavigationStack.Count - 2;
			    
			    var lastPage = _navigation.NavigationStack[index] as ContentPage;

			    return lastPage;
		    }

		    return null;
	    }
    }
}