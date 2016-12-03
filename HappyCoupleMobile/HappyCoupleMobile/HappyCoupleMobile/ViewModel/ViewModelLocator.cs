/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:HappyCoupleMobile"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace HappyCoupleMobile.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<ToDoListViewModel>(true);
        }

        public MainViewModel Main => GetViewModel<MainViewModel>();
        public ToDoListViewModel ToDoListViewModel => GetViewModel<ToDoListViewModel>();

        public T GetViewModel<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public static void Cleanup()
        {
        }
    }
}