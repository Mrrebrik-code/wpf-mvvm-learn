using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
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
		#region SelectedGroup [Group]

		private Group _selectedGroup;
		public Group SelectedGroup
		{
			get => _selectedGroup;
			set
			{
				if (!Set(ref _selectedGroup, value)) return;

				_selectedGroupStudents.Source = value?.Students;
				OnPropertyChanged(nameof(SelectedGroupStudent));
			}
		}

		private readonly CollectionViewSource _selectedGroupStudents = new CollectionViewSource();
		public ICollectionView SelectedGroupStudent => _selectedGroupStudents?.View;

		#endregion

		private string _filterText;
		public string FilterText
		{
			get => _filterText;
			set => Set(ref _filterText, value);
		}

		#region SelectedCompositionObject [Object]

		private object _selectedCompositionObject;
		public Object SelectedCompositionObject
		{
			get => _selectedCompositionObject;
			set => Set(ref _selectedCompositionObject, value);
		}

		#endregion
		#region CompositionColection [Object]

		public object[] CompositionCollection { get; set; }

		#endregion
		#region Groups [ObservableCollection<Group>]

		public ObservableCollection<Group> Groups { get; set; }

		#endregion

		private static Random _random = new Random();

		private IEnumerable<Student> _studentsTestingCollection = Enumerable.Range(1, 1000).Select(i => new Student
		{
			Name = $"Name_{_random.Next(0, 9999)}",
			Surname = $"Surname{_random.Next(0, 9999)}"
		});

		public IEnumerable<Student> StudentsTestingCollection
		{
			get => _studentsTestingCollection;
			set => Set(ref _studentsTestingCollection, value);
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
		#region Create New Group Command

		public ICommand CreateNewGroupCommand { get; }

		private bool CanCreateNewGroupCommandExecute(object parametr) => true;
		private void OnCreateNewGroupCommandExecuted(object parametr)
		{

			var groupMaxIndex = Groups.Count + 1;
			var newGroup = new Group
			{
				Name = $"Group {groupMaxIndex}",
				Students = new ObservableCollection<Student>(),
				Description = $"Description {groupMaxIndex}"
			};

			Groups.Add(newGroup);

		}

		#endregion
		#region Delete Group Command

		public ICommand DeleteGroupCommand { get; }

		private bool CanDeleteGroupCommandExecute(object parametr) => parametr is Group group && Groups.Contains(group);
		private void OnDeleteGroupCommandExecuted(object parametr)
		{
			if (!(parametr is Group group)) return;

			int indexGroupLast = Groups.IndexOf(group) - 1;
			if(indexGroupLast >= 0) SelectedGroup = Groups[indexGroupLast];
			
			Groups.Remove(group);
		}

		#endregion

		public ICommand SeratchFilterTextStudents { get; }

		private bool CanSeratchFilterTextStudents(object parametr) => true;
		private void OnSeratchFilterTextStudents(object parametr)
		{
			_selectedGroupStudents.View.Refresh();
		}
		private void FilterHandler(object sender, FilterEventArgs e)
		{
			if (!(e.Item is Student student))
			{
				e.Accepted = false;
				return;
			}

			if(student.Name is null || student.Surname is null || student.Patronymic is null)
			{
				e.Accepted = false;
				return;
			}

			var filterText = _filterText;
			if (string.IsNullOrWhiteSpace(filterText)) return;


			if (student.Name.Contains(filterText)) return;
			if (student.Surname.Contains(filterText)) return;
			if (student.Patronymic.Contains(filterText)) return;

			e.Accepted = false;

		}
		#endregion


		public MainWindowViewModel()
		{
			#region Create Commands

			CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
			SetSelectedIndexCommand = new LambdaCommand(OnSetSelectedIndexCommandExecuted, CanSetSelectedIndexCommandExecute);
			CreateNewGroupCommand = new LambdaCommand(OnCreateNewGroupCommandExecuted, CanCreateNewGroupCommandExecute);
			DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);
			SeratchFilterTextStudents = new LambdaCommand(OnSeratchFilterTextStudents, CanSeratchFilterTextStudents);

			#endregion

			#region Generation Random Data From Testing

			List<DataPointModel> dataPoints = GenerationRandomTestData();
			TestDataPoints = dataPoints;

			CreatePlotModelFromTargetListData(dataPoints);

			#endregion

			CreateCollectionGroupFromStudents();

			CreateCompositionObjectsFromArray();

			_selectedGroupStudents.Filter += FilterHandler;
		}

	

		private void CreateCompositionObjectsFromArray()
		{
			var listData = new List<Object>();

			listData.Add("Hello World!");
			listData.Add(20);
			listData.Add(Groups[0]);
			listData.Add(Groups[0].Students[0]);

			CompositionCollection = listData.ToArray();
		}

		private void CreateCollectionGroupFromStudents()
		{
			int studentIndex = 1;

			IEnumerable<Student> students = Enumerable.Range(1, 10).Select(i => new Student
			{
				Name = $"Name {studentIndex}",
				Surname = $"Surname {studentIndex}",
				Patronymic = $"Patronymic {studentIndex}",
				Description = $"Description {studentIndex++}",
				Birthday = DateTime.Now,
				Rating = 0
			});

			IEnumerable<Group> groups = Enumerable.Range(1, 200).Select(i => new Group
			{
				Name = $"Group {i}",
				Description = $"Description {i}",
				Students = new ObservableCollection<Student>(students)
			});

			Groups = new ObservableCollection<Group>(groups);
			 
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
