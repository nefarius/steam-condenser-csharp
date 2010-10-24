/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Collections.Generic;
using System.Xml;

namespace SteamCondenser.Steam.Community
{
	public abstract class GameClass
	{
		public string Name     { get; protected set; }
		public int    PlayTime { get; protected set; }
		
		protected GameClass() { }
		
		public GameClass(XmlElement data)
		{
			Name     =           data.GetInnerText("className");
			PlayTime = int.Parse(data.GetInnerText("playtimeSeconds"));
		}
	}
	
	public class GameAchievement
	{
		public int      AppID     { get; protected set; }
		public string   Name      { get; protected set; }
		public long     SteamID64 { get; protected set; }
		public DateTime Timestamp { get; protected set; }
		public bool     Unlocked  { get; protected set; }
		
		protected XmlElement Data { get; set; }
		
		
		internal GameAchievement(long steamid64, int appid, XmlElement data)
		{
			SteamID64 = steamid64;
			AppID     = appid;
			Data      = data;
			
			Name      = data.GetInnerText("name");
			Unlocked  = data.Attributes["closed"] != null;
			
			if (data.GetElementsByTagName("unlockTimestamp").Count > 0)
			{
				Timestamp = new DateTime(Convert.ToInt32(data.GetInnerText("unlockTimestamp")));
			}
		}
	}
	
	public abstract class GameWeapon 
	{
		public string ID    { get; protected set; }
		public int    Kills { get; protected set; }
		public int    Shots { get; protected set; }
		
		public float AverageShotsPerKill {
			get { 
				if (Kills == 0) return 0.0f;
				return (float)Shots / Kills;
			}
		}
		
		public GameWeapon(XmlElement data)
		{
			Kills = int.Parse(data.GetInnerText("kills"));
		}
	}
	
	public class GameStats
	{
		public const string AppUrl = "http://store.steampowered.com/app/";
			
		#region Game Names
		/// <summary>
		/// Short game name retrieved from the web
		/// </summary>
		public string GameFriendlyName { get; protected set; }
		/// <summary>
		/// Long game name retrieved from the web
		/// </summary>
		public string GameName         { get; protected set; }
		/// <summary>
		/// game name specified by the user in the constructors
		/// </summary>
		public string ShortGameName    { get; protected set; }
		
		#endregion
		public int    AchievmentsDone  { get; protected set; }
		public int    AppID            { get; protected set; }
		public string CustomUrl        { get; protected set; }
		public string HoursPlayed      { get; protected set; }
		public string PrivacyState     { get; protected set; }
		public long   SteamID64        { get; protected set; }
		
		public GameAchievement[] Achievments { get; protected set; }
		
		public bool IsPublic { get { return PrivacyState.Equals("public"); } }
		public float AchievmentsPercentage { get { return (float)AchievmentsDone/Achievments.Length; } }
		
		public string BaseUrl {
			get {
				if (CustomUrl == null)
					return SteamID.GetPage(SteamID64) + "/stats/" + ShortGameName + "?xml=1";
				else
					return SteamID.GetPage(CustomUrl) + "/stats/" + ShortGameName + "?xml=1";
			}
		}
		
		public static GameStats Create(string steamid, string gamename)
		{
			switch (gamename)
			{
			case TF2Stats.AppName:
				return new TF2Stats(steamid);
			case CSSStats.AppName:
				return new CSSStats(steamid);
			default:
				return new GameStats(steamid, gamename);
			}
		}
		
		public static GameStats Create(long steamid, string gamename)
		{
			switch (gamename)
			{
			case TF2Stats.AppName:
				return new TF2Stats(steamid);
			case CSSStats.AppName:
				return new CSSStats(steamid);
			default:
				return new GameStats(steamid, gamename);
			}
		}
		
		public static GameStats Create(SteamID steamid, string gamename)
		{
			if (steamid.CustomUrl != null)
				return Create(steamid.CustomUrl, gamename);
			else
				return Create(steamid.SteamID64, gamename);
		}
		
		protected XmlDocument doc;
		protected GameStats(string steamid, string gamename)
		{
			CustomUrl = steamid;
			ShortGameName = gamename;
			
			doc = new XmlDocument();
			doc.LoadUrl(BaseUrl);
			FetchData();			
		}
		
		protected GameStats(long steamid, string gamename)
		{
			SteamID64 = steamid;
			
			ShortGameName = gamename;
			
			doc = new XmlDocument();
			doc.LoadUrl(BaseUrl);
			FetchData();			
		}
		
		protected void FetchData()
		{
			
			PrivacyState = doc.GetInnerText("privacyState");
				
			if (IsPublic)
			{
				var gameNode = doc.GetElementsByTagName("game").Item(0) as XmlElement;
				
				AppID =  int.Parse(gameNode.GetInnerText("gameLink").Replace(AppUrl, ""));
				GameFriendlyName = gameNode.GetInnerText("gameFriendlyName");
				GameName = 		   gameNode.GetInnerText("gameName");
				
				var achievments = (doc.GetElementsByTagName("achievements").Item(0) as XmlElement).GetElementsByTagName("achievement");
				
				List<GameAchievement> list = new List<GameAchievement>();
				foreach (XmlElement achievment in achievments)
				{
					list.Add(new GameAchievement(SteamID64, AppID, achievment));
				}
				
				Achievments = list.ToArray();
				
			}
		}
		
	}
	
}
