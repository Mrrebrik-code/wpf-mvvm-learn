using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
	internal class MainWindowViewModel : ViewModel
	{
		#region Tittle [String]

		private string _title = "CV19";
		public string Tittle
		{
			get => _title;
			set => Set(ref _title, value);
		}

		#endregion
		#region Status [String]

		private string _status = "Done";
		public string Status
		{
			get => _status;
			set => Set(ref _status, value);
		}

		#endregion
	}
}
