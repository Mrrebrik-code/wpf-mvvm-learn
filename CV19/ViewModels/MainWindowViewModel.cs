using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
	internal class MainWindowViewModel : ViewModel
	{
		#region Properties Fields

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

		private IEnumerable<DataPointModel> _testDataPoints;
		public IEnumerable<DataPointModel> TestDataPoints
		{
			get => _testDataPoints;
			set => Set(ref _testDataPoints, value);
		}

		#endregion
		#region CurrentSelectedIndexTabControl [int]

		private int _currentSelectedIndexTabControl;
		public int CurrentSelectedIndexTabControl
		{
			get => _currentSelectedIndexTabControl;
			set
			{
				if (value > 6 || value < 0)
				{
					return;
				}
				

				Set(ref _currentSelectedIndexTabControl, value);
			}
		}

		#endregion
		#region MyModel [PlotModel]
		public PlotModel MyModel { get; private set; }

		#endregion

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
		#region Set Selected Index TabControl Command
		public ICommand SetSelectedIndexCommand { get; }
		private bool CanSetSelectedIndexCommandExecute(object parameter) => true;
		private void OnSetSelectedIndexCommandExecuted(object parameter)
		{
			if (parameter == null) return;

			var index = Convert.ToInt32(parameter);
			CurrentSelectedIndexTabControl += index;
		}
		#endregion

		#endregion

	
		public MainWindowViewModel()
		{
			#region Create Commands

			CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
			SetSelectedIndexCommand = new LambdaCommand(OnSetSelectedIndexCommandExecuted, CanSetSelectedIndexCommandExecute);

			#endregion

			#region Generation Random Data From Testing

			List<DataPointModel> dataPoints = GenerationRandomTestData();
			TestDataPoints = dataPoints;

			CreatePlotModelFromTargetListData(dataPoints);

			#endregion
		}

		private void CreatePlotModelFromTargetListData(List<DataPointModel> dataPoints)
		{
			MyModel = new PlotModel { Title = "Какой-то график :D" };
			LineSeries lineSeries = new LineSeries();
			for (int i = 0; i < dataPoints.Count; i++)
			{
				DataPoint dataPoint = new DataPoint(dataPoints[i].XValue, dataPoints[i].YValue);
				lineSeries.Points.Add(dataPoint);
			}

			MyModel.Series.Add(lineSeries);
		}

		private List<DataPointModel> GenerationRandomTestData()
		{
			List<DataPointModel> dataPoints = new List<DataPointModel>((int)(360 / 0.45));
			const double toRad = Math.PI / 75;

			for (double x = 0d; x <= 360; x += 0.25d)
			{
				double y = Math.Sin(x * toRad);

				dataPoints.Add(new DataPointModel { XValue = x, YValue = y });
			}

			return dataPoints;
		}
	}
}
