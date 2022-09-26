using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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
		#region TestDataPoints [IEnumerable]

		private IEnumerable<DataPoint> _testDataPoints;
		public IEnumerable<DataPoint> TestDataPoints
		{
			get => _testDataPoints;
			set => Set(ref _testDataPoints, value);
		}

		#endregion

		#region Commands

		#region Close Application Command

		public ICommand CloseApplicationCommand { get; }

		private bool CanCloseApplicationCommandExecute(object parameter) => true;
		private void OnCloseApplicationCommandExecuted(object parameter)
		{
			Application.Current.Shutdown();
		}
		  
		#endregion

		#endregion


		public MainWindowViewModel()
		{
			#region Create Commands

			CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

			#endregion

			var dataPoints = new List<DataPoint>((int) (360 / 0.1));
			const double toRad = Math.PI / 180;

			for (var x = 0d; x <= 360; x += 0.1d)
			{
				var y = Math.Sin(x * toRad);

				dataPoints.Add(new DataPoint { XValue = x, YValue = y });
			}

			TestDataPoints = dataPoints;
		}
	}
}
