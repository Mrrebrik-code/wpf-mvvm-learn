using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CV19Console
{
	internal class Program
	{
		private const string _dataUrl = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
		static void Main(string[] args)
		{
			//WebClient client = new WebClient();

			var client = new HttpClient();
			var response = client.GetAsync(_dataUrl).Result;
			var csv = response.Content.ReadAsStringAsync().Result;

			Console.WriteLine(csv.ToString());

			Console.ReadLine();
		}
	}
}
