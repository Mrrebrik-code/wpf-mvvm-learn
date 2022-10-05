using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerTest.Models
{
	internal class Server
	{
		private IPAddress _adress;
		private int _port;

		private TcpListener _server = default;

		public Server(string ip, string port)
		{
			_adress = IPAddress.Parse(ip);
			_port = int.Parse(port);

			_server = new TcpListener(_adress, _port);
		}

		public void Start()
		{
			Task.Run(() =>
			{
				StartServer();
			});
		}

		private void StartServer()
		{
			_server.Start();

			try
			{
				while (true)
				{
					TcpClient client = _server.AcceptTcpClient();

					NetworkStream stream = client.GetStream();

					string responseDefault = "Hello World!";

					byte[] data = Encoding.UTF8.GetBytes(responseDefault);

					stream.Write(data, 0, data.Length);

					stream.Close();
					client.Close();
				}

			}
			catch (Exception exeption)
			{
				MessageBox.Show(exeption.Message);
			}
			finally
			{
				if (_server != null) _server.Stop();
			}
		}
	}
}
