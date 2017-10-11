using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
	public class ExtendedListView : ListView
	{
		public static BindableProperty ItemTappedCommandProperty =
			BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(ExtendedListView));

		public ICommand ItemTappedCommand
		{
			get => (ICommand)GetValue(ItemTappedCommandProperty);
			set => SetValue(ItemTappedCommandProperty, value);
		}

		public ExtendedListView()
		{
			ItemTapped += OnItemTapped;
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
		{
			if (ItemTappedCommand != null && ItemTappedCommand.CanExecute(itemTappedEventArgs.Item))
			{
				ItemTappedCommand.Execute(itemTappedEventArgs.Item);
			}

			await Task.Delay(300);
			SelectedItem = null;
		}
	}
}
