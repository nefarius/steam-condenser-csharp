/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Collections.Generic;
using System.Xml;
	
namespace SteamCondenser.Steam.Community
{
	public class CSSMap
	{
		public string Name         { get; protected set; }
		public bool   Favorite     { get; protected set; }
		public int    RoundsPlayed { get; protected set; }
		public int    RoundsLost   { get; protected set; }
		public int    RoundsWon    { get; protected set; }
		public float  RoundsWonPercentage { get; protected set; }
		
		
		public CSSMap(string mapname, XmlElement data)
		{
			Name = mapname;
			Favorite = data.GetInnerText("favorite").Equals(Name);
			RoundsPlayed = int.Parse(data.GetInnerText(Name + "_rounds"));
			RoundsWon = int.Parse(data.GetInnerText(Name + "_wins"));
			RoundsLost = RoundsPlayed - RoundsWon;
			
			RoundsWonPercentage = (RoundsPlayed > 0) ? ((float)RoundsWon / RoundsPlayed) : 0;
		}
	}
	
	public class CSSWeapon
	{
		public string Name     { get; protected set; }
		public float  Accuracy { get; protected set; }
		public float  KSRatio  { get; protected set; }
		public bool   Favorite { get; protected set; }
		public int    Hits     { get; protected set; }
		public int    Shots    { get; protected set; }
		public int    Kills    { get; protected set; }
		
		public CSSWeapon(string weaponname, XmlElement data)
		{
			Name = weaponname;

			if ((Name != "grenade") && (Name != "knife"))
			{
				Favorite = data.GetInnerText("favorite").Equals(Name);
				Kills = int.Parse(data.GetInnerText(Name + "_kills"));
				Shots = int.Parse(data.GetInnerText(Name + "_shots"));
				
				if (Shots != 0)
				{
					Accuracy = Hits / Shots;
					KSRatio = Kills / Shots;
				}
				else
				{
					Accuracy = 0;
					KSRatio = 0;
				}
			}
		}
	}
	
	public class CSSLastMatchStats
	{
		public float  CostPerKill    { get; protected set; }
		public int    CTWins         { get; protected set; }
		public int    Damage         { get; protected set; }
		public int    Deaths         { get; protected set; }
		public int    Dominations    { get; protected set; }
		public int    FavoriteWeapon { get; protected set; }
		public int    Kills          { get; protected set; }
		public int    MaxPlayers     { get; protected set; }
		public int    Money          { get; protected set; }
		public int    Revenges       { get; protected set; }
		public int    Stars          { get; protected set; }
		public int    TWins          { get; protected set; }
		public int    Wins           { get; protected set; }
		
		public float  KDRatio        { get; protected set; }
		
		public CSSLastMatchStats(XmlElement data)
		{
			CostPerKill    = float.Parse(data.GetInnerText("costkill"));
			CTWins         =   int.Parse(data.GetInnerText("ct_wins"));
			Damage         =   int.Parse(data.GetInnerText("dmg"));
			Deaths         =   int.Parse(data.GetInnerText("deaths"));
			Dominations    =   int.Parse(data.GetInnerText("dominations"));
			FavoriteWeapon =   int.Parse(data.GetInnerText("favwpnid"));
			Kills          =   int.Parse(data.GetInnerText("kills"));
			MaxPlayers     =   int.Parse(data.GetInnerText("max_players"));
			Money          =   int.Parse(data.GetInnerText("money"));
			Revenges       =   int.Parse(data.GetInnerText("revenges"));
			Stars          =   int.Parse(data.GetInnerText("stars"));
			TWins          =   int.Parse(data.GetInnerText("t_wins"));
			Wins           =   int.Parse(data.GetInnerText("wins"));
			
			if (Deaths == 0)
				KDRatio = 0;
			else
				KDRatio = Kills / Deaths;
		}
	}
	
	
	public class CSSStats : GameStats
	{
		public const string AppName = "cs:s";
		
		private static string[] maps = new string[] { 
			"cs_assault", "cs_compound", "cs_havana", "cs_italy", "cs_militia", "cs_office",
			"de_aztec", "de_cbble", "de_chateau", "de_dust", "de_dust2", "de_inferno", "de_nuke",
			"de_piranesi", "de_port", "de_prodigy", "de_tides", "de_train" };

	    private static string[] weapons = new string[] { 
			"deagle", "usp", "glock", "p228", "elite", "fiveseven", "awp", "ak47", "m4a1", "aug",
			"sg552", "sg550", "galil", "famas", "scout", "g3sg1", "p90", "mp5navy", "tmp", "mac10",
			"ump45", "m3", "xm1014", "m249", "knife", "grenade" };
		
		public CSSLastMatchStats LastMatch  { get; protected set; }
		
		public int BlindKills          { get; protected set; }
		public int BombsDefused        { get; protected set; }
		public int BombsPlanted        { get; protected set; }
		public int Damage              { get; protected set; }
		public int Deaths              { get; protected set; }
		public int DominationOverKills { get; protected set; }
		public int Dominations         { get; protected set; }
		public int EarnedMoney         { get; protected set; }
		public int EnemyWeaponKills    { get; protected set; }
		public int Hits                { get; protected set; }
		public int HostagesRescued     { get; protected set; }
		public int Kills               { get; protected set; }
		public int KinfeKills          { get; protected set; }
		public int LogosSprayed        { get; protected set; }
		public int NightvisionDamage   { get; protected set; }
		public int PistolRoundsWon     { get; protected set; }
		public int Revenges            { get; protected set; }
		public int RoundsPlayed        { get; protected set; }
		public int RoundsWon           { get; protected set; }
		public int SecondsPlayed       { get; protected set; }
		public int Shots               { get; protected set; }
		public int Stars               { get; protected set; }
		public int WeaponsDonated      { get; protected set; }
		public int WindowsBroken       { get; protected set; }
		public int ZoomedSniperKills   { get; protected set; }
		
		public float  Accuracy         { get; protected set; }
		public float  KDRatio          { get; protected set; }
		public int    RoundsLost       { get; protected set; }
		
		public CSSMap[]    MapStats    { get; protected set; }
		public CSSWeapon[] WeaponStats { get; protected set; }
		
		public CSSStats(string steamid)
			: base(steamid, AppName)
		{
			FetchData();
		}
		
		public CSSStats(long steamid)
			: base(steamid, AppName)
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			var stats = doc.GetXmlElement("stats");
			LastMatch = new CSSLastMatchStats(stats.GetXmlElement("lastmatch"));

			var lifeTimeStats = stats.GetXmlElement("lifetime");
			var summaryStats  = stats.GetXmlElement("summary");
			
			BlindKills          = int.Parse(lifeTimeStats.GetInnerText("blindkills"));
			BombsDefused        = int.Parse(lifeTimeStats.GetInnerText("bombsdefused"));
			Damage              = int.Parse(lifeTimeStats.GetInnerText("dmg"));
			Deaths              = int.Parse( summaryStats.GetInnerText("deaths"));
			DominationOverKills = int.Parse(lifeTimeStats.GetInnerText("dominationoverkills"));
			Dominations         = int.Parse(lifeTimeStats.GetInnerText("dominations"));
			EarnedMoney         = int.Parse(lifeTimeStats.GetInnerText("money"));
			Hits                = int.Parse( summaryStats.GetInnerText("shotshit"));
			HostagesRescued     = int.Parse(lifeTimeStats.GetInnerText("hostagesrescued"));
			Kills               = int.Parse( summaryStats.GetInnerText("kills"));
			KinfeKills          = int.Parse(lifeTimeStats.GetInnerText("knifekills"));
			LogosSprayed        = int.Parse(lifeTimeStats.GetInnerText("decals"));
			NightvisionDamage   = int.Parse(lifeTimeStats.GetInnerText("nvgdmg"));
			PistolRoundsWon     = int.Parse(lifeTimeStats.GetInnerText("pistolrounds"));
			Revenges            = int.Parse(lifeTimeStats.GetInnerText("revenges"));
			RoundsPlayed        = int.Parse( summaryStats.GetInnerText("rounds"));
			RoundsWon           = int.Parse( summaryStats.GetInnerText("wins"));
			SecondsPlayed       = int.Parse( summaryStats.GetInnerText("timeplayed"));
			Shots               = int.Parse( summaryStats.GetInnerText("shots"));
			Stars               = int.Parse( summaryStats.GetInnerText("stars"));
			WeaponsDonated      = int.Parse(lifeTimeStats.GetInnerText("wpndonated"));
			WindowsBroken       = int.Parse(lifeTimeStats.GetInnerText("winbroken"));
			ZoomedSniperKills   = int.Parse(lifeTimeStats.GetInnerText("zsniperkills"));
			
			if (Shots == 0)
				Accuracy = 0;
			else
				Accuracy = (float)Hits/Shots;
			
			if (Deaths == 0)
				KDRatio = (float)Kills/Deaths;
			
			RoundsLost = RoundsPlayed - RoundsWon;
			
			List<CSSMap> mapList = new List<CSSMap>();
			var mapData = doc.GetXmlElement("stats").GetXmlElement("maps");
			foreach (string map in maps) {
				mapList.Add(new CSSMap(map, mapData));
			}
			MapStats = mapList.ToArray();
			
			List<CSSWeapon> weaponList = new List<CSSWeapon>();
			var weaponData = doc.GetXmlElement("stats").GetXmlElement("weapons");
			foreach (string weapon in weapons) {
				weaponList.Add(new CSSWeapon(weapon, weaponData));
			}
			
			WeaponStats = weaponList.ToArray();
		}
	}
}
