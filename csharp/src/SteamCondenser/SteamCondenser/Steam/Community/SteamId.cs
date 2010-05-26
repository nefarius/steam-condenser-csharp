
using System;
using System.Text.RegularExpressions;

namespace SteamCondenser.Steam.Community
{
	public class SteamId
	{
		public string CustomUrl { get; protected set; }
		public string FavoriteGame { get; protected set; }
		public float FavoriteGameHoursPlayed { get; protected set; }
		public long FetchTime { get; set; }
		
		protected SteamId[] friends = null;
		public SteamId[] Friends {
			get {
				if (this.friends == null) FetchFriends();
				return friends;
			}
		}
		
		private void FetchFriends()
		{
			
		}

		public static string ConvertCommunityIdToSteamId(long communityId)
		{
			long steamId1 = communityId % 2;
			long steamId2 = communityId - 76561197960265728L;
			
			if (steamId2 <= 0) 
			{
				throw new SteamCondenserException("SteamID " + communityId + " is too small.");
			}
			steamId2 = (steamId2 - steamId1) / 2;
			return "STEAM_0:" + steamId1 + ":" + steamId2;
		}
		
		private static Regex steamIdRegex = new Regex(@"^STEAM_[0-1]:[0-1]:[0-9]+$", RegexOptions.Compiled);
		public static long convertSteamIdToCommunityId(string steamId)
		{
			if (steamId.Equals("STEAM_ID_LAN") || steamId.Equals("BOT")) 
			{
				throw new Exception("Cannot convert SteamID \"" + steamId + "\" to a community ID.");
			}
			if (!steamIdRegex.Match(steamId).Success)
			{
				throw new SteamCondenserException("SteamID \"" + steamId + "\" doesn't have the correct format.");
			}
			
			string[] tmpId = steamId.Substring(6).Split(new char [] { ':' });
			return long.Parse(tmpId[1]) + long.Parse(tmpId[2]) * 2 + 76561197960265728L;
			
			
		}
		
	}
}
