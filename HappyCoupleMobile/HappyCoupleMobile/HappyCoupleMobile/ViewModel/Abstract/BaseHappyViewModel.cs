using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Mvvm.Messages;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.Services;
using HappyCoupleMobile.Services.Interfaces;
using HappyCoupleMobile.View;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.ViewModel.Abstract
{
    public abstract class BaseHappyViewModel : ViewModelBase
    {
        private INavigationPageService _navigationService;
        private readonly ISimpleAuthService _simpleAuthService;

        protected abstract Task OnNavigateTo(IMessageData message);

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


        public void ShowAlertMessage(AlertType alertType, string message)
        {
            Device.BeginInvokeOnMainThread(() => MessengerInstance.Send<IAlertMessage>(new AlertMessage(message, alertType)));
        }

        public void RegisterNavigateToMessage<TVm>(TVm viewModel, bool unRegister = false)
        {
            if (unRegister)
            {
                MessengerInstance.Register<IBaseMessage<TVm>>(viewModel, async (message) => await OnRecievedMessageWithUnregistration<TVm>(message));
            }
            else
            {
                MessengerInstance.Register<IBaseMessage<TVm>>(viewModel, async (message) => await OnRecievedMessageWithoutUnregistration<TVm>(message));
            }

        }

        protected virtual async Task OnFeedback(IFeedbackMessage feedbackMessage)
        {
            await Task.Yield();
        }

        private async Task OnRecievedMessageWithUnregistration<TVm>(IBaseMessage<TVm> message)
        {
            await OnNavigateTo(message, true);
        }

        private async Task OnRecievedMessageWithoutUnregistration<TVm>(IBaseMessage<TVm> message)
        {
            await OnNavigateTo(message, false);
        }

        private async Task OnNavigateTo<TVm>(IBaseMessage<TVm> messasge, bool unRegister)
        {
            if (unRegister)
            {
                MessengerInstance.Unregister<IBaseMessage<TVm>>(this);
            }
            await OnNavigateTo(messasge);
        }

        public async Task NavigateTo<TV, TVm>() where TV : ContentPage, new()
        {
            SendEmptyMessage<TVm>();

            await NavigationService.PushAsync<TV>();
        }

        public async Task NavigateToWithMessage<TV, TVm>(IBaseMessage<TVm> message) where TV : ContentPage, new()
        {
            SendMessage<TVm>(message);

            await NavigationService.PushAsync<TV>();
        }

        public void SendEmptyMessage<TVm>()
        {
            IBaseMessage<TVm> message = new BaseMessage<TVm>();

            MessengerInstance.Send(message);
        }

        public void SendMessage<TVm>(IBaseMessage<TVm> message)
        {
            MessengerInstance.Send(message);
        }

        public async Task SendFeedbackMessage(IFeedbackMessage feedbackMessage)
        {
	        await NavigationService.GetLastViewModelFromStack().OnFeedback(feedbackMessage);
        }
	    
	    public async Task SendFeedbackMessage<TOwner>(IFeedbackMessage feedbackMessage) where TOwner : BaseHappyViewModel
	    {
		    var viewModel =  NavigationService.GetFirstOrDefaultViewModelFromStack<TOwner>();

		    if (viewModel == null)
		    {
			    return;
		    }

		    await viewModel.OnFeedback(feedbackMessage);
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

        protected virtual async Task OnGoBackCommand()
        {
            CleanResources();
            await NavigateBack();
        }

        protected virtual void CleanResources()
        {

        }
    }
}