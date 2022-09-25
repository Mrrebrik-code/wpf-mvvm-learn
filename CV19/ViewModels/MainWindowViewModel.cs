using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
	internal class MainWindowViewModel : ViewModel
	{
		private string _title = "CV19";
		public string Tittle
		{
			get => _title;
			set => Set(ref _title, value);
		}
	}
}
