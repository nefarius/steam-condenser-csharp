using System;
using NUnit.Framework;
using SteamCondenser.Steam.Community;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class ID
	{
		[Test]
		public void Converting()
		{
			Assert.AreEqual(SteamID.ConvertSteamIDToCommunityID("STEAM_0:0:14722800"),
			                76561197989711328L);

			Assert.AreEqual(SteamID.ConvertCommunityIDToSteamID(76561197989711328),
			                "STEAM_0:0:14722800");
		}
	}
}