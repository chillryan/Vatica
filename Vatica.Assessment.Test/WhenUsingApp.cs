namespace Vatica.Assessment.Test
{
	using System;
	using NUnit.Framework;
	using Vatica.Assessment.App;

	[TestFixture]
	public class WhenUsingApp
	{
		[Test]
		[TestCase("GOOG")]
		[TestCase("MSFT")]
		public void GetStockExchangeForTicker_TickersAreValid_ReturnsExchanges(string q)
		{
			var exchange = Program.GetStockExchangeForTicker(q);

			Assert.IsNotEmpty(exchange);
		}

		[Test]
		[TestCase("Ryan's Stock")]
		[TestCase(" ")]
		[TestCase(null)]
		public void GetStockExchnageForTicker_InvalidTicker_ReturnsNull(string q)
		{
			var exchange = Program.GetStockExchangeForTicker(q);

			Assert.IsNull(exchange);
		}
	}
}