using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Chat
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs eventMouseButton)
		{
			if (eventMouseButton.LeftButton == MouseButtonState.Pressed) DragMove();
		}

		private void Close_Button(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void Minimize_Button(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		private void WindowState_Click(object sender, RoutedEventArgs e)
		{
			if (Application.Current.MainWindow.WindowState != WindowState.Maximized) Application.Current.MainWindow.WindowState = WindowState.Maximized;
			else Application.Current.MainWindow.WindowState = WindowState.Normal;
		}
	}
}
