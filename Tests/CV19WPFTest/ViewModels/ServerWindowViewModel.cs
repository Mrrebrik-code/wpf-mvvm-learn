﻿using ServerTest.ViewModels.Base;

namespace ServerTest.ViewModels
{
	internal class ServerWindowViewModel : ViewModel
	{
		private string _textStatusApp;

		#region Server
		private string _ipAdressServer = "127.0.0.1";
		public string IpAdressServer
		{
			get => _ipAdressServer;
			set => Set(ref _ipAdressServer, value);
		}

		private string _portServer = "25565";
		public string PortServer
		{
			get => _portServer;
			set => Set(ref _portServer, value);
		}

		private string _textContentFromMessangerServer;
		public string TextContentFromMessangerServer
		{
			get => _textContentFromMessangerServer;
			set => Set(ref _textContentFromMessangerServer, value);
		}
		private string _textMessageFromSendingServer;
		public string TextMessageFromSendingServer
		{
			get => _textMessageFromSendingServer;
			set => Set(ref _textMessageFromSendingServer, value);
		}

		private string _textButtonListener = "Start Listener!";
		public string TextButtonListener
		{
			get => _textButtonListener;
			set => Set(ref _textButtonListener, value);
		}
		#endregion

		#region Client
		private string _ipAdressClient = "127.0.0.1";
		public string IpAdressClient
		{
			get => _ipAdressClient;
			set => Set(ref _ipAdressClient, value);
		}

		private string _portClient = "25565";
		public string PortClient
		{
			get => _portClient;
			set => Set(ref _portClient, value);
		}

		private string _textContentFromMessangerClient;
		public string TextContentFromMessangerClient
		{
			get => _textContentFromMessangerClient;
			set => Set(ref _textContentFromMessangerClient, value);
		}

		private string _textMessageFromSendingClient;
		public string TextMessageFromSendingClient
		{
			get => _textMessageFromSendingClient;
			set => Set(ref _textMessageFromSendingClient, value);
		}
		#endregion
	}
}
