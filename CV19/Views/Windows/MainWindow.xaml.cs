using CV19.Models.Decanat;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CV19
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
		{
			if (!(e.Item is Group group)) return;
			if (group.Name is null) return;

			var filterText = GroupNameFilterText.Text;

			if (filterText.Length == 0) return;

			if (group.Name.Contains(filterText)) return;
			if (group.Description.Contains(filterText)) return;

			e.Accepted = false;
		}

		private void Click_SearchButton(object sender, RoutedEventArgs e)
		{
			var button = (Button)sender;
			var groupCollection = (CollectionViewSource)button.FindResource("GroupsCollection");
			groupCollection.View.Refresh();
		}
	}
}
