using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HappyCoupleMobile.ViewModel
{
    public class ToDoListViewModel : ViewModelBase
    {
        private string _hello;

        public string Hello
        {
            get { return _hello; }
            set { Set(ref _hello, value); }
        }

        public ToDoListViewModel()
        {
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            Hello = "Test";
        }
    }
}
