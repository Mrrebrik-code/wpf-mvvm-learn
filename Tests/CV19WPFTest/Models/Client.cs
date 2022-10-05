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
	internal class Client
	{
		private string _adress;
		private int _port;

		TcpClient _client = default;

        public Action<string> OnMessage;

		public void Connect(string ip, string port, Action<string> callback)
		{
            OnMessage += callback;
            _adress = ip;
			_port = int.Parse(port);

            try
            {
                _client = new TcpClient();
                _client.Connect(_adress, _port);

                byte[] data = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = _client.GetStream();

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    string message = Encoding.UTF8.GetString(data, 0, bytes);
                    OnMessageCallback(message);
                }
                while (stream.DataAvailable);

                // Закрываем потоки
                stream.Close();
                _client.Close();
            }
            catch (SocketException scoketExeption)
            {
                MessageBox.Show(scoketExeption.Message);
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }


        public void OnMessageCallback(string message)
		{
            OnMessage?.Invoke(message);
        }
	}
}
