namespace Vatica.Assessment.App
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Text;
	using System.Threading.Tasks;

	public class Program
	{
		private static HttpClient _client = new HttpClient()
		{
			BaseAddress = new Uri("https://www.google.com"),
		};

		public static string GetStockExchangeForTicker(string quoteTicker)
		{
			string result = null;
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


			try
			{
				var response = _client.GetAsync($"finance/info?q={quoteTicker}").Result;
				if (response.IsSuccessStatusCode)
				{
					var content = response.Content.ReadAsStringAsync().Result.Replace("//", ""); // google sends us malformed json so let's clean it up
					var token = JToken.Parse(content);
					result = token.Root.Select(t => (string)t["e"]).FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		static void Main(string[] args)
		{
			string entry;
			do
			{
				Console.Write("Enter quote (or exit to quit): ");
				entry = Console.ReadLine();
				var tickerExchange = GetStockExchangeForTicker(entry);
				Console.WriteLine($"Exchange: {tickerExchange}");
			} while (!entry.Equals("exit", StringComparison.InvariantCultureIgnoreCase));
		}
	}
}
