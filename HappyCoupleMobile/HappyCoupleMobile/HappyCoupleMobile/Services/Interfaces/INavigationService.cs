using System.Threading.Tasks;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.Services.Interfaces
{
    public interface INavigationPageService
    {
        Task PushAsync<T>() where T : ContentPage, new();
        Task PopAsync();
        Task GoToRoot();
	    BaseHappyViewModel GetLastViewModelFromStack();
	    TOwner GetFirstOrDefaultViewModelFromStack<TOwner>() where TOwner : BaseHappyViewModel;
    }
}