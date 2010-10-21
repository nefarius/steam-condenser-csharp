/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

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
		private static Dictionary<object, SteamID> cache = new Dictionary<object, SteamID>();
		
		public string    CustomUrl               { get; protected set; }
		public string    FavoriteGame            { get; protected set; }
		public float     FavoriteGameHoursPlayed { get; protected set; }
		public DateTime  FetchTime               { get; protected set; }
		//public SteamId[] Friends                 { get; protected set; }
		// map
		public string    Location                { get; protected set; }
		// SteamGroup
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
		
		public Dictionary<string, string> Links { get; protected set; }
		public Dictionary<string, float> MostPlayedGames { get; protected set; }
		
		public bool IsFetched { get { return FetchTime.Ticks == 0; } }
		
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
		
		public string BaseUrl {
			get {
				if (CustomUrl == null)
					return "http://steamcommunity.com/profiles/" + SteamID64;
				else
					return "http://steamcommunity.com/id/" + CustomUrl;
			}
		}
		
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
			
			if (steamId2 <= 0) 
			{
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
		
		public static SteamID Create(long id)
		{
			return SteamID.Create((object)id, true, false);
		}
		
		public static SteamID Create(string id)
		{
			return SteamID.Create((object)id, true, false);
		}
		
		public static SteamID Create(long id, bool fetch)
		{
			return SteamID.Create((object)id, fetch, false);
		}
		
		public static SteamID Create(string id, bool fetch)
		{
			return SteamID.Create((object)id, fetch, false);
		}
		
		public static SteamID Create(long id, bool fetch, bool bypassCache)
		{
			return SteamID.Create((object)id, fetch, bypassCache);
		}
		
		public static SteamID Create(string id, bool fetch, bool bypassCache)
		{
			return SteamID.Create((object)id, fetch, bypassCache);
		}		
		
		private static SteamID Create(Object id, bool fetch, bool bypassCache)
		{
			if (IsCached(id) && !bypassCache)
			{
				SteamID steamid = cache[id];
				if (fetch && !steamid.IsFetched)
				{
					steamid.FetchData();
				}
				return steamid;
			} 
			else 
				return new SteamID(id, fetch);
		}
		
		public static bool IsCached(Object id)
		{
			return cache.ContainsKey(id);
		}
		public bool Cached
		{
			get { return IsCached(SteamID64); }
		}
		
		private SteamID(Object id, bool fetch)
		{
			if (id is string)
				this.CustomUrl = (string)id;
			else
				this.SteamID64 = (long)id;
			
			if (fetch) FetchData();
		}
		
		public void FetchData()
		{
			string url = BaseUrl + "?xml=1";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			XmlDocument profile = new XmlDocument();
			profile.Load(request.GetResponse().GetResponseStream());
			
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
			{
				throw new SteamCondenserException(profile.GetInnerText("privacyMessage"));
			}
			
			if (PrivacyState == "public")
			{
				CustomUrl = profile.GetInnerText("customURL");
				if (CustomUrl.Length == 0) CustomUrl = null;
			}
			
			
			var favGame = profile.GetElementsByTagName("favoriteGame").Item(0);
			if (favGame != null)
			{
				// TODO: implement this
			}
			
			var mostPlayedGamesNode = profile.GetElementsByTagName("mostPlayedGames").Item(0);
			MostPlayedGames = new Dictionary<string, float>();
			if (mostPlayedGamesNode != null)
			{
				foreach (XmlElement node in mostPlayedGamesNode)
				{
					string gameName   =             node.GetInnerText("gameName");
					float hoursPlayed = float.Parse(node.GetInnerText("hoursPlayed"));
					MostPlayedGames.Add(gameName, hoursPlayed);
				}
			}
			
			var groupsNode = profile.GetElementsByTagName("groups").Item(0);
			if (groupsNode != null)
			{
				foreach (XmlElement node in groupsNode)
				{
					long groupid = long.Parse(node.GetInnerText("groupID64"));
					// TODO: do something with the data
				}
			}

			var weblinksNode = profile.GetElementsByTagName("weblinks").Item(0);
			Links = new Dictionary<string, string>();
			if (groupsNode != null)
			{
				foreach (XmlElement node in weblinksNode)
				{
					string title = node.GetInnerText("title");
					string link  = node.GetInnerText("link");
					Links.Add(title, link);
				}
			}
			
			
			FetchTime = DateTime.Now;
		}
		
		public long[] GetFriendsIDs()
		{
			string url = BaseUrl + "/friends?xml=1";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			XmlDocument page = new XmlDocument();
			page.Load(request.GetResponse().GetResponseStream());
			
			var friends = page.GetElementsByTagName("friends").Item(0);
			
			long[] friendids = new long[friends.ChildNodes.Count];
			for (int i = 0; i < friends.ChildNodes.Count; i++)
			{
				friendids[i] = long.Parse(friends.ChildNodes[i].InnerText);
			}
			return friendids;
		}
		
		private void FetchFriends()
		{
			var friendsids = GetFriendsIDs();
			friends = new SteamID[friendsids.Length];
			for (int i = 0; i < friendsids.Length; i++)
			{
				friends[i] = SteamID.Create(friendsids[i], false);
			}
		}
		
	}
}
