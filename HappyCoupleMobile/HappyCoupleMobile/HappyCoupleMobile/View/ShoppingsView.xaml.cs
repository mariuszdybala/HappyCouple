using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.View.Abstract;
using Xamarin.Forms;

namespace HappyCoupleMobile.View
{
    public partial class ShoppingsView : BaseHappyContentPage
    {
        public ICommand ActiveTabTappedCommand { get; set; }
        public ICommand ClosedTabTappedCommand { get; set; }

        public ShoppingsView()
        {
            InitializeComponent();

            ActiveTabTappedCommand = new RelayCommand(OnActiveTabTappedCommand);
            ClosedTabTappedCommand = new RelayCommand(OnClosedTabTappedCommand);
        }

        private void OnClosedTabTappedCommand()
        {
        }

        private void OnActiveTabTappedCommand()
        {

        }

        private void SwitchTabs()
        {
            
        }
    }
}
