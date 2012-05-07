using System;
using NUnit.Framework;
using SteamCondenser.Steam.Community;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class CounterStrikeSource
	{
		[Test]
		public void CustomUrl()
		{
			CSSStats stats = new CSSStats("koraktor");

			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Achievments.Length, 0);

			stats = SteamID.Create("koraktor").CSSStats;

			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Achievments.Length, 0);


		}

		[Test]
		public void SteamID64()
		{
			CSSStats stats = new CSSStats(76561197961384956);

			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Achievments.Length, 0);

			stats = SteamID.Create(76561197961384956).CSSStats;

			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Achievments.Length, 0);
		}
	}
}

