using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SteamCondenser.Steam.Community
{
	/// <summary>
	/// The SteamID class represents a Steam Community profile (also called Steam ID)
	/// </summary>
	public class SteamID
	{
		#region Main Fields

		private static Dictionary<object, SteamID> cacheMemory = new Dictionary<object, SteamID>();

		public string    CustomUrl               { get; protected set; }
		public string    FavoriteGame            { get; protected set; }
		public float     FavoriteGameHoursPlayed { get; protected set; }
		public DateTime  FetchTime               { get; protected set; }
		//public SteamId[] Friends                 { get; protected set; }
		// map
		public string    Location                { get; protected set; }
		public string    Headline                { get; protected set; }
		public float     HoursPlayed             { get; protected set; }
		public string    ImageUrl                { get; protected set; }
		public DateTime  MemberSince             { get; protected set; }
		public string    OnlineState             { get; protected set; }
		public string    PrivacyState            { get; protected set; }
		public string    Realname                { get; protected set; }
		public string    StateMessage            { get; protected set; }
		public string    Nickname                { get; protected set; }
		public long      SteamID64               { get; protected set; }
		public float     SteamRating             { get; protected set; }
		public string    SteamRatingText         { get; protected set; }
		public string    Summary                 { get; protected set; }
		public bool      VacBanned               { get; protected set; }
		public int       VisibilityState         { get; protected set; }

		public SteamGroup[] Groups { get; protected set; }
		public Dictionary<string, string> Links { get; protected set; }
		public Dictionary<string, float> MostPlayedGames { get; protected set; }

		public bool IsFetched { get { return FetchTime.Ticks != 0; } }

		protected SteamID[] friends = null;
		public SteamID[] Friends {
			get {
				if (this.friends == null) FetchFriends();
				return friends;
			}
		}

		public string AvatarFullUrl   { get { return ImageUrl + "_full.jpg";   } }
		public string AvatarIconUrl   { get { return ImageUrl + ".jpg";        } }
		public string AvatarMediumUrl { get { return ImageUrl + "_medium.jpg"; } }

		public bool IsOnline { get { return (OnlineState.Equals("online") || InGame); } }
		public bool InGame { get { return OnlineState.Equals("in-game"); } }

		#endregion

		#region CommunityID and SteamID

		/// <summary>
		/// Converts the 64bit SteamID as used and reported by the Steam Community to a SteamID
		/// reported by game servers
		/// </summary>
		/// <param name="communityId">
		/// 64bit SteamID <see cref="System.Int64"/>
		/// </param>
		/// <returns>
		/// Gameserver STteamID <see cref="System.String"/>
		/// </returns>
		public static string ConvertCommunityIDToSteamID(long communityId)
		{
			long steamId1 = communityId % 2;
			long steamId2 = communityId - 76561197960265728L;

			if (steamId2 <= 0) {
				throw new SteamCondenserException("SteamID " + communityId + " is too small.");
			}
			steamId2 = (steamId2 - steamId1) / 2;
			return "STEAM_0:" + steamId1 + ":" + steamId2;
		}

		private static Regex steamIdRegex = new Regex(@"^STEAM_[0-1]:[0-1]:[0-9]+$", RegexOptions.Compiled);
		/// <summary>
		/// Converts the SteamID as reported by game servers to a 64bit SteamID
		/// </summary>
		/// <param name="steamId">
		/// Gameserver SteamID <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// 64bit SteamID <see cref="System.Int64"/>
		/// </returns>
		public static long ConvertSteamIDToCommunityID(string steamId)
		{
			if (steamId.Equals("STEAM_ID_LAN") || steamId.Equals("BOT"))
				throw new Exception("Cannot convert SteamID \"" + steamId + "\" to a community ID.");

			if (!steamIdRegex.Match(steamId).Success)
				throw new SteamCondenserException("SteamID \"" + steamId + "\" doesn't have the correct format.");

			string[] tmpId = steamId.Substring(6).Split(new char [] { ':' });
			return long.Parse(tmpId[1]) + long.Parse(tmpId[2]) * 2 + 76561197960265728L;
		}

		#endregion

		#region Constructors

		public static SteamID Create(long id)
		{
			return SteamID.Create(id, true, true);
		}

		public static SteamID Create(string id)
		{
			return SteamID.Create(id, true, true);
		}

		public static SteamID Create(long id, bool fetch)
		{
			return SteamID.Create(id, fetch, true);
		}

		public static SteamID Create(string id, bool fetch)
		{
			return SteamID.Create(id, fetch, true);
		}

		public static SteamID Create(long id, bool fetch, bool cache)
		{
			return SteamID.Create((object)id, fetch, cache);
		}

		public static SteamID Create(string id, bool fetch, bool cache)
		{
			return SteamID.Create((object)id.ToLower(), fetch, cache);
		}

		private static SteamID Create(Object id, bool fetch, bool cache)
		{
			SteamID steamid;

			if (!cache) {
				steamid = new SteamID(id);
			} else if (!IsCached(id)) {
				steamid = new SteamID(id);
				cacheMemory[id] = steamid;
			} else {
				steamid = cacheMemory[id];
			}

			if (fetch && !steamid.IsFetched) steamid.FetchData(cache);

			return steamid;

		}

		private SteamID(Object id)
		{
			if (id is string)
				this.CustomUrl = (string)id;
			else
				this.SteamID64 = (long)id;
		}

		#endregion

		#region URL handling

		public static string ProfileIDPage   { get { return "http://steamcommunity.com/profiles/"; } }
		public static string ProfilePage { get { return "http://steamcommunity.com/id/"; } }

		public static string GetPage(long id)
		{
			return ProfileIDPage + id;
		}

		public static string GetPage(string id)
		{
			return ProfilePage + id;
		}

		public string ProfileUrl   { get { return SteamID.GetPage(CustomUrl); } }
		public string ProfileIDUrl { get { return SteamID.GetPage(SteamID64); } }

		public string BaseUrl {
			get {
				if (CustomUrl == null) return ProfileIDUrl;
				else return ProfileUrl;
			}
		}

		#endregion

		#region Caching

		public static bool IsCached(Object id)
		{
			return cacheMemory.ContainsKey(id);
		}

		public void Cache()
		{
			if (!cacheMemory.ContainsKey(this.SteamID64))
				cacheMemory[SteamID64] = this;

			if ((CustomUrl != null) && !cacheMemory.ContainsKey(CustomUrl))
				cacheMemory[CustomUrl] = this;
		}

		public static void ClearCache()
		{
			cacheMemory = new Dictionary<object, SteamID>();
		}

		#endregion

		public void FetchData()
		{
			FetchData(true);
		}

		public void FetchData(bool cache)
		{
			try {
				XmlDocument profile = new XmlDocument();
				profile.LoadUrl(BaseUrl + "?xml=1");

				// TODO: check for error, throw exception


				Nickname        =                profile.GetInnerText("steamID");
				SteamID64       =     long.Parse(profile.GetInnerText("steamID64"));
				VacBanned       =                profile.GetInnerText("vacBanned").Equals("1");
				ImageUrl        =                profile.GetInnerText("avatarIcon");
				OnlineState     =                profile.GetInnerText("onlineState");
				PrivacyState    =                profile.GetInnerText("privacyState");
				StateMessage    =                profile.GetInnerText("stateMessage");
				VisibilityState =      int.Parse(profile.GetInnerText("visibilityState"));
				Headline        =                profile.GetInnerText("headline");
				HoursPlayed     =    float.Parse(profile.GetInnerText("hoursPlayed2Wk"));
				Location        =                profile.GetInnerText("location");
				MemberSince     = DateTime.Parse(profile.GetInnerText("memberSince"));
				Realname        =                profile.GetInnerText("realname");
				SteamRating     =    float.Parse(profile.GetInnerText("steamRating"));
				Summary         =                profile.GetInnerText("summary");

				if (profile.GetElementsByTagName("privacyMessage").Count > 0)
					throw new SteamCondenserException(profile.GetInnerText("privacyMessage"));

				if (PrivacyState == "public") {
					CustomUrl = profile.GetInnerText("customURL");
					if (CustomUrl.Length == 0) CustomUrl = null;
					else Cache();
				}


				var favGame = profile.GetElementsByTagName("favoriteGame").Item(0);
				if (favGame != null)
				{
					// TODO: implement this
				}

				var mostPlayedGamesNode = profile.GetElementsByTagName("mostPlayedGames").Item(0);
				MostPlayedGames = new Dictionary<string, float>();
				if (mostPlayedGamesNode != null) {
					foreach (XmlElement node in mostPlayedGamesNode) {
						string gameName   =             node.GetInnerText("gameName");
						float hoursPlayed = float.Parse(node.GetInnerText("hoursPlayed"));
						MostPlayedGames.Add(gameName, hoursPlayed);
					}
				}

				var groupsNode = profile.GetElementsByTagName("groups").Item(0);
				if (groupsNode != null) {
					List<SteamGroup> grps = new List<SteamGroup>();
					foreach (XmlElement node in groupsNode) {
						grps.Add(SteamGroup.Create(long.Parse(node.GetInnerText("groupID64")), false));
					}
					Groups = grps.ToArray();
				}

				var weblinksNode = profile.GetXmlElement("weblinks");
				if (weblinksNode != null) {
					Links = new Dictionary<string, string>();
					if (groupsNode != null) {
						foreach (XmlElement node in weblinksNode) {
							string title = node.GetInnerText("title");
							string link  = node.GetInnerText("link");
							Links.Add(title, link);
						}
					}
				} else { }
			} catch (XmlException) {
				throw new SteamCondenserException("XML data could not be parsed.");
			}
			FetchTime = DateTime.Now;
		}

		public long[] GetFriendsIDs()
		{
			XmlDocument page = new XmlDocument();
			page.LoadUrl(BaseUrl + "/friends?xml=1");

			var friends = page.GetElementsByTagName("friends").Item(0);

			long[] friendids = new long[friends.ChildNodes.Count];
			for (int i = 0; i < friends.ChildNodes.Count; i++) {
				friendids[i] = long.Parse(friends.ChildNodes[i].InnerText);
			}
			return friendids;
		}

		private void FetchFriends()
		{
			var friendsids = GetFriendsIDs();
			friends = new SteamID[friendsids.Length];
			for (int i = 0; i < friendsids.Length; i++) {
				friends[i] = SteamID.Create(friendsids[i], false);
			}
		}

		#region GameStats

		public GameStats GameStats(string gamename)
		{
			if (CustomUrl != null)
				return Steam.Community.GameStats.Create(CustomUrl, gamename);
			else
				return Steam.Community.GameStats.Create(SteamID64, gamename);
		}

		public TF2Stats TF2Stats {
			get {
				if (CustomUrl != null)
					return new Steam.Community.TF2Stats(CustomUrl);
				else
					return new Steam.Community.TF2Stats(SteamID64);
			}
		}

		public CSSStats CSSStats {
			get {
				if (CustomUrl != null)
					return new Steam.Community.CSSStats(CustomUrl);
				else
					return new Steam.Community.CSSStats(SteamID64);
			}
		}

		public L4DStats L4DStats {
			get {
				if (CustomUrl != null)
					return new Steam.Community.L4DStats(CustomUrl);
				else
					return new Steam.Community.L4DStats(SteamID64);
			}
		}

		public L4D2Stats L4D2Stats {
			get {
				if (CustomUrl != null)
					return new Steam.Community.L4D2Stats(CustomUrl);
				else
					return new Steam.Community.L4D2Stats(SteamID64);
			}
		}

		public AlienSwarmStats AlienSwarmStats {
			get {
				if (CustomUrl != null)
					return new Steam.Community.AlienSwarmStats(CustomUrl);
				else
					return new Steam.Community.AlienSwarmStats(SteamID64);
			}
		}

		#endregion

	}
}
