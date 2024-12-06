using System.Windows;
using PZ_18.ViewModels;
using PZ_18.Views;
using PZ_18.Models;

namespace PZ_18
{
	/// <summary>
	/// Главное окно приложения.
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void AddRequest_Click(object sender, RoutedEventArgs e)
		{
			var addWindow = new AddEditRequestWindow();
			addWindow.ShowDialog();

			if (DataContext is MainViewModel vm)
			{
				vm.SearchRequests(null);
			}
		}

		private void EditRequest_Click(object sender, RoutedEventArgs e)
		{
			if (RequestDataGrid.SelectedItem is Request selectedRequest)
			{
				var editWindow = new AddEditRequestWindow(selectedRequest);
				editWindow.ShowDialog();

				if (DataContext is MainViewModel vm)
				{
					vm.SearchRequests(null);
				}
			}
			else
			{
				MessageBox.Show("Выберите заявку для редактирования");
			}
		}
	}
}
