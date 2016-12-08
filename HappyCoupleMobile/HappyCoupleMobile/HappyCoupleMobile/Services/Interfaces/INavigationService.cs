using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappyCoupleMobile.Services
{
    public interface INavigationPageService
    {
        Task PushAsync<T>() where T : ContentPage, new();
        Task PopAsync();
        Task GoToRoot();
    }
}