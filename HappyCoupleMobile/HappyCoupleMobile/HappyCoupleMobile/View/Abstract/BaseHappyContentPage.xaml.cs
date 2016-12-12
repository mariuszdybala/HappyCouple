using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.ViewModel.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.View.Abstract
{
    public partial class BaseHappyContentPage : ContentPage
    {
        public BaseHappyContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        public TViewModel GetBoundViewModel<TViewModel>() where TViewModel : ViewModelBase
        {
            return BindingContext as TViewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = GetBoundViewModel<BaseHappyViewModel>();
            viewModel?.ViewAppeared(this, EventArgs.Empty);


            Messenger.Default.Unregister<IAlertMessage>(this);
            Messenger.Default.Register<IAlertMessage>(this, async msg => await DisplayAlert(msg));
        }

        protected override void OnDisappearing()
        {
            Messenger.Default.Unregister<IAlertMessage>(this);
        }

        protected async Task DisplayAlert(IAlertMessage alertMessage)
        {
            bool result = true;
            if (alertMessage.AlertType == AlertType.Confirmation)
            {
                result = await DisplayAlert("Confirm", alertMessage.Message, "Yes", "No");
            }
            else
            {
                await DisplayAlert(alertMessage.AlertType.ToString(), alertMessage.Message, "OK");
            }
        }
    }
}
