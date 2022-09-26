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
			var russia = GetData().First(data => data.country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

			Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia.counts, (date, count) => $"{date:dd.MM.yy} - {count}")));
			
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

						yield return line.Replace("Korea,", "Korea -");
					}
				}
			}
		}

		private static DateTime[] GetDates()
		{
			DateTime[] lines = GetDataLines()
			   .First()
			   .Split(',')
			   .Skip(4)
			   .Select(text => DateTime.Parse(text, CultureInfo.InvariantCulture))
			   .ToArray();

			return lines;
		}

		private static IEnumerable<(string country, string province, int[] counts)> GetData()
		{
			IEnumerable<string[]> lines = GetDataLines().Skip(1).Select(line => line.Split(','));

			foreach (var line in lines)
			{
				string provinceName = line[0].Trim();
				string countryName = line[1].Trim(' ', '"');

				int[] count = null;
				try { count = line.Skip(4).Select(int.Parse).ToArray(); }
				catch {}
				

				yield return (countryName, provinceName, count);
			}
		}
	}
}
