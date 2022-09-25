using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
			DateTime[] dates = GetDates();
			Console.WriteLine(string.Join("\r\n", dates));
			
			Console.ReadLine();
		}

		private static async Task<Stream> GetDataStream()
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync(_dataUrl, HttpCompletionOption.ResponseHeadersRead);

			return await response.Content.ReadAsStreamAsync();
		}

		private static IEnumerable<string> GetDataLines()
		{
			using (Stream dataStream = GetDataStream().Result)
			{
				using (StreamReader dataReader = new StreamReader(dataStream))
				{
					while (!dataReader.EndOfStream)
					{
						string line = dataReader.ReadLine();
						if (string.IsNullOrWhiteSpace(line) == true) continue;

						yield return line;
					}
				}
			}
		}

		private static DateTime[] GetDates() => GetDataLines()
			.First()
			.Split(',')
			.Skip(4)
			.Select(text => DateTime.Parse(text, CultureInfo.InvariantCulture))
			.ToArray();
	}
}
