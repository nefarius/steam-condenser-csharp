/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Xml;
using System.Collections.Generic;

namespace SteamCondenser.Steam.Community
{
	
	public class L4DMostRecentGame	
	{
		
		public string Difficulty { get; protected set; }
		public bool   Escaped    { get; protected set; }
		public string Movie      { get; protected set; }
		public string TimePlayed { get; protected set; }
		
		public L4DMostRecentGame(XmlElement data)
		{
			Difficulty = data.GetInnerText("difficulty");
			Escaped    = data.GetInnerText("bEscaped").Equals("1");
			Movie      = data.GetInnerText("movie");
			// TODO: check this out, maybe it's because i didn't played for a long time
			TimePlayed = data.GetInnerText("time");
		}
	}
	
	public class L4DFavourite
	{
		public string Campaign               { get; protected set; }
		public int    CampaignPercentage     { get; protected set; }
		public string Character              { get; protected set; }
		public int    CharacterPercentage    { get; protected set; }
		public string Level1Weapon           { get; protected set; }
		public int    Level1WeaponPercentage { get; protected set; }
		public string Level2Weapon           { get; protected set; }
		public int    Level2WeaponPercentage { get; protected set; }
		
		public L4DFavourite(XmlElement data)
		{
			Campaign               =           data.GetInnerText("campaign");
			CampaignPercentage     = int.Parse(data.GetInnerText("campaignpct"));
			Character              =           data.GetInnerText("character");
			CharacterPercentage    = int.Parse(data.GetInnerText("characterpct"));
			Level1Weapon           =           data.GetInnerText("weapon1");
			Level1WeaponPercentage = int.Parse(data.GetInnerText("weapon1pct"));
			Level2Weapon           =           data.GetInnerText("weapon2");
			Level2WeaponPercentage = int.Parse(data.GetInnerText("weapon2pct"));
			
			
		}
	}
	
	public class L4DSurvivalStats
	{
		public int   GoldMedals   { get; protected set; }
		public int   SolverMedals { get; protected set; }
		public int   BronzeMedals { get; protected set; }
		public int   RoundsPlayed { get; protected set; }
		public float BestTime     { get; protected set; }
		
		public L4DSurvivalStats(XmlElement data)
		{
			GoldMedals   =   int.Parse(data.GetInnerText("goldmedals"));
			SolverMedals =   int.Parse(data.GetInnerText("silvermedals"));
			BronzeMedals =   int.Parse(data.GetInnerText("bronzemedals"));
			RoundsPlayed =   int.Parse(data.GetInnerText("roundsplayed"));
			BestTime     = float.Parse(data.GetInnerText("besttime"));
		}
	}
	
	public class L4DTeamPlayStats
	{
		public int    Revived                 { get; protected set; }
		public string MostRevivedDifficulty   { get; protected set; }
		public float  AverageRevived          { get; protected set; }
		public float  AverageWasRevived       { get; protected set; }
		public int    Protected               { get; protected set; }
		public string MostProtectedDifficulty { get; protected set; }
		public float  AverageProtected        { get; protected set; }
		public float  AverageWasProtected     { get; protected set; }
		public int    FriendlyFireDamage      { get; protected set; } 
		// TODO: rename this long name
		public string MostFriendlyFireDamageDifficulty { get; protected set; }
		public float  AverageFriendlyFireDamage { get; protected set; }
		
		
		public L4DTeamPlayStats(XmlElement data)
		{
			Revived                 =   int.Parse(data.GetInnerText("revived"));
			MostRevivedDifficulty   =             data.GetInnerText("reviveddiff");
			AverageRevived          = float.Parse(data.GetInnerText("revivedavg"));
			AverageWasRevived       = float.Parse(data.GetInnerText("wasrevivedavg"));
			Protected               =   int.Parse(data.GetInnerText("protected"));
			MostProtectedDifficulty =             data.GetInnerText("protecteddiff");
			AverageProtected        = float.Parse(data.GetInnerText("protectedavg"));
			AverageWasProtected     = float.Parse(data.GetInnerText("wasprotectedavg"));
			FriendlyFireDamage      =   int.Parse(data.GetInnerText("ffdamage"));
			
			MostFriendlyFireDamageDifficulty =    data.GetInnerText("ffdamagediff");
			AverageFriendlyFireDamage=float.Parse(data.GetInnerText("ffdamageavg"));
		}
	}
	
	public class L4DVersusStats
	{
		public int    GamesPlayed               { get; protected set; }
		public int    GamesCompleted            { get; protected set; }
		public int    FinalesSurvived           { get; protected set; }
		public float  FinalesSurvivedPercentage { get; protected set; }
		public int    Points                    { get; protected set; }
		public string MostPointsInfected        { get; protected set; }
		public int    GamesWon                  { get; protected set; }
		public int    GamesLost                 { get; protected set; }
		public int    HighestSurvivorScore      { get; protected set; }
		
		public L4DVersusStats(XmlElement data)
		{
			GamesPlayed = int.Parse(data.GetInnerText("gamesplayed"));
			GamesCompleted = int.Parse(data.GetInnerText("gamescompleted"));
			FinalesSurvived = int.Parse(data.GetInnerText("finales"));
			FinalesSurvivedPercentage = (float)FinalesSurvived/GamesPlayed;
			Points = int.Parse(data.GetInnerText("points"));
			MostPointsInfected = data.GetInnerText("pointsas");
			GamesWon = int.Parse(data.GetInnerText("gameswon"));
			GamesLost = int.Parse(data.GetInnerText("gameslost"));
			HighestSurvivorScore = int.Parse(data.GetInnerText("survivorscore"));
			                     
		}
	}
	
	public abstract class AbstractL4DStats : GameStats
	{
		
		public L4DMostRecentGame MostRecentGame { get; protected set; }
		public L4DFavourite      Favourite      { get; protected set; }
		public L4DSurvivalStats  SurvivalStats  { get; protected set; }
		public L4DTeamPlayStats  TeamPlayStats  { get; protected set; }
		public L4DVersusStats    VersusStats    { get; protected set; }
		
		// life time stats
		
		public int    FinalesSurvived    { get; protected set; }
		public int    GamesPlayed        { get; protected set; }
		public int    InfectedKilled     { get; protected set; }
		public float  KillsPerHour       { get; protected set; }
		public float  AverageKitsShared  { get; protected set; }
		public float  AverageKitsUsed    { get; protected set; }
		public float  AveragePillsShared { get; protected set; }
		public float  AveragePillsUsed   { get; protected set; }
		public string TimePlayed         { get; protected set; }
		
		public float FinalesSurvivedPercentage { get; protected set; }
		
		
		public AbstractL4DStats(string steamid, string gamename)
			: base(steamid, gamename)
		{
			FetchData();
		}
		
		public AbstractL4DStats(long steamid, string gamename)
			: base(steamid, gamename)
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			if (IsPublic)
			{
				var stats = doc.GetXmlElement("stats");
				
				// TODO: check if node is empty (== null)
				var mostRecentGame = stats.GetXmlElement("mostrecentgame");
				
				if (mostRecentGame.InnerText != string.Empty)
					MostRecentGame = new L4DMostRecentGame(mostRecentGame);
				else 
					mostRecentGame = null;
				
				Favourite        = new      L4DFavourite(stats.GetXmlElement("favorites"));
				SurvivalStats    = new  L4DSurvivalStats(stats.GetXmlElement("survival"));
				TeamPlayStats    = new  L4DTeamPlayStats(stats.GetXmlElement("teamplay"));
				VersusStats      = new    L4DVersusStats(stats.GetXmlElement("versus"));
				
				var lifeTime = stats.GetXmlElement("lifetime");
				
				FinalesSurvived    =   int.Parse(lifeTime.GetInnerText("finales"));
				GamesPlayed        =   int.Parse(lifeTime.GetInnerText("gamesplayed"));
				InfectedKilled     =   int.Parse(lifeTime.GetInnerText("infectedkilled"));
				KillsPerHour       = float.Parse(lifeTime.GetInnerText("killsperhour"));
				AverageKitsShared  = float.Parse(lifeTime.GetInnerText("kitsshared"));
				AverageKitsUsed    = float.Parse(lifeTime.GetInnerText("kitsused"));
				AveragePillsShared = float.Parse(lifeTime.GetInnerText("pillsshared"));
				AveragePillsUsed   = float.Parse(lifeTime.GetInnerText("pillsused"));
				
				TimePlayed = lifeTime.GetInnerText("timeplayed");
				
				FinalesSurvivedPercentage = (float)FinalesSurvived/GamesPlayed;	
			}
		}
		
	}

	public abstract class AbstractL4DWeapon : GameWeapon
	{
		public static float ParsePercentage(string pct)
		{
			return float.Parse(pct.Substring(0, pct.Length - 1)) / 100;
		}
		
		public string Name               { get; protected set; }
		public float  Accuracy           { get; protected set; }
		public float  HeadShotPercentage { get; protected set; }
		public float  KillPercentage     { get; protected set; }
			
		public AbstractL4DWeapon(XmlElement data)
			: base(data)
		{
			Name  = data.Name;

			Shots              =       int.Parse(data.GetInnerText("shots"));
			HeadShotPercentage = ParsePercentage(data.GetInnerText("headshots"));
			Accuracy           = ParsePercentage(data.GetInnerText("accuracy"));
		}
	}

	# region Left4Dead
	
	public class L4DMap
	{
		public enum Medals { None, Bronze, Silver, Gold };
		
		public static Medals MedalFrom(string medal)
		{
			switch (medal)
			{
			case "gold":
				return Medals.Gold;
			case "silver":
				return Medals.Silver;
			case "bronze":
				return Medals.Bronze;
			default:
				return Medals.None;
			}
		}
		
		public static Medals MedalFrom(int medal)
		{
			return (Medals)medal;
		}
		 
		public float  BestTime    { get; protected set; }
		public string ID          { get; protected set; }
		public Medals Medal       { get; protected set; }
		public int    TimesPlayed { get; protected set; }
		public string Name        { get; protected set; }
		
		protected L4DMap() { }
		
		public L4DMap(XmlElement data)
		{
			ID = data.Name;
			Name     =             data.GetInnerText("name");
			BestTime = float.Parse(data.GetInnerText("besttimeseconds"));
			Medal    =   MedalFrom(data.GetInnerText("medal"));
		}
	}
	
	public class L4DExplosive : GameWeapon
	{
		public static bool IsExplosive(string weaponname)
		{
			return ((weaponname == "molotov") || (weaponname == "pipes"));
		}
		
		public L4DExplosive(XmlElement data)
			: base(data)
		{
			ID = data.Name;
			Shots = int.Parse(data.GetInnerText("thrown"));
		}
		
		public float AverageKillsPerShot { get { return 1 / AverageShotsPerKill; } }
	}
	
	public class L4DWeapon : AbstractL4DWeapon
	{
		public L4DWeapon(XmlElement data)
			: base(data)
		{
			KillPercentage = ParsePercentage(data.GetInnerText("killpct"));
		}
	}
	
	public class L4DStats : AbstractL4DStats
	{
		public L4DMap[] MapStats { get; protected set; }
		public GameWeapon[] WeaponStats { get; protected set; }
		
		public L4DStats(string steamid)
			: base(steamid, "l4d")
		{
			FetchData();
		}
		
		public L4DStats(long steamid)
			: base(steamid, "l4d")
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			if (IsPublic) {
				var stats = doc.GetXmlElement("stats");
				
				var survivalStats = stats.GetXmlElement("survival");
				List<L4DMap> mapList = new List<L4DMap>();
				foreach (XmlElement map in survivalStats.GetXmlElement("maps")) {
					mapList.Add(new L4DMap(map));
				}
				MapStats = mapList.ToArray();
				
				var weaponsStats = stats.GetXmlElement("weapons");
				List<GameWeapon> weaponList = new List<GameWeapon>();
				foreach (XmlElement weapon in weaponsStats) {
					if (L4DExplosive.IsExplosive(weapon.Name))
						weaponList.Add(new L4DExplosive(weapon));
					else
						weaponList.Add(new L4DWeapon(weapon));
				}
				WeaponStats = weaponList.ToArray();
			}
		}
	}
	
	#endregion
	
	#region Left4Dead2
	
	public class L4D2ScavengeMapStats
	{
		public int    AverageRoundScore { get; protected set; }
		public int    HighestGameScore  { get; protected set; }
		public int    HighestRoundScore { get; protected set; }
		public string Name              { get; protected set; }
		public int    RoundsPlayed      { get; protected set; }
		public int    RoundsWon         { get; protected set; }
		
		public L4D2ScavengeMapStats(XmlElement data)
		{
			AverageRoundScore = int.Parse(data.GetInnerText("avgscoreperround"));
			HighestGameScore  = int.Parse(data.GetInnerText("highgamescore"));
			HighestRoundScore = int.Parse(data.GetInnerText("highroundscore"));
			Name              =           data.GetInnerText("fullname");
			RoundsPlayed      = int.Parse(data.GetInnerText("roundsplayed"));
			RoundsWon         = int.Parse(data.GetInnerText("roundswon"));
		}
	}
	
	public class L4D2ScavengeStats
	{
		public float AverageCansPerRound { get; protected set; }
		public int   PerfectRounds       { get; protected set; }
		public int   RoundsLost          { get; protected set; }
		public int   RoundsPlayed        { get; protected set; }
		public int   RoundsWon           { get; protected set; }
		public int   TotalCans           { get; protected set; }
		
		public L4D2ScavengeMapStats[] MapStats { get; protected set; }
		
		public L4D2ScavengeStats(XmlElement data)
		{
			AverageCansPerRound = float.Parse(data.GetInnerText("avgcansperround"));
			PerfectRounds       = int.Parse(data.GetInnerText("perfect16canrounds"));
			RoundsLost          = int.Parse(data.GetInnerText("roundslost"));
			RoundsPlayed        = int.Parse(data.GetInnerText("roundsplayed"));
			RoundsWon           = int.Parse(data.GetInnerText("roundswon"));
			TotalCans           = int.Parse(data.GetInnerText("totalcans"));
			
			List<L4D2ScavengeMapStats> mapList = new List<L4D2ScavengeMapStats>();
			foreach (XmlElement map in data.GetXmlElement("mapstats")) {
				mapList.Add(new L4D2ScavengeMapStats(map));
			}
			MapStats = mapList.ToArray();
		}
	}
	
	public class L4D2Items
	{
		private static string[] items = { "adrenaline", "defibs", "medkits", "pills"};
		
		public static string[] Items { get { return items; } }
		
		public static bool IsItem(string name)
		{
			foreach (string itemname in Items) {
				if (itemname == name)
					return true;
			}
			return false;
		}
		
		public int Adrenaline    { get; protected set; }
		public int Defibrilators { get; protected set; }
		public int MedKits       { get; protected set; }
		public int Pills         { get; protected set; }
		
		public L4D2Items(XmlElement data)
		{
			Adrenaline    = int.Parse(data.GetInnerText("items_adrenaline"));
			Defibrilators = int.Parse(data.GetInnerText("items_defibs"));
			MedKits       = int.Parse(data.GetInnerText("items_medkits"));
			Pills         = int.Parse(data.GetInnerText("items_pills"));
		}
		
	}
	
	public class L4D2Infected
	{
		private static string[] infected = { "boomer", "charger", "common", "hunter", "jockey", "smoker", "spitter", "tank" };
		
		public static string[] Infected { get { return infected; } }
		
		public static bool IsInfected(string name)
		{
			foreach (string infectedname in Infected) {
				if (infectedname == name)
					return true;
			}
			return false;
		}
		
		public int Boomer  { get; protected set; }
		public int Charger { get; protected set; }
		public int Common  { get; protected set; }
		public int Hunter  { get; protected set; }
		public int Jockey  { get; protected set; }
		public int Smoker  { get; protected set; }
		public int Spitter { get; protected set; }
		public int Tank    { get; protected set; }
		
		public L4D2Infected(XmlElement data)
		{
			Boomer  = int.Parse(data.GetInnerText("kills_boomer"));
			Charger = int.Parse(data.GetInnerText("kills_charger"));
			Common  = int.Parse(data.GetInnerText("kills_common"));
			Hunter  = int.Parse(data.GetInnerText("kills_hunter"));
			Jockey  = int.Parse(data.GetInnerText("kills_jockey"));
			Smoker  = int.Parse(data.GetInnerText("kills_smoker"));
			Spitter = int.Parse(data.GetInnerText("kills_spitter"));
			Tank    = int.Parse(data.GetInnerText("kills_tank"));
			
		}
		
	}
	
	public class L4D2Map : L4DMap
	{
		public bool      Played    { get; protected set; }
		public L4D2Items ItemsUsed { get; protected set; }
		public SteamID[] Teammates { get; protected set; }
		
		public L4D2Infected InfectedKilled { get; protected set; }
		
		public L4D2Map(XmlElement data)
		{
			string imgurl = data.GetInnerText("img");
			ID = imgurl.Substring(0, imgurl.LastIndexOf('/') -4);
			Name = data.GetInnerText("name");
			Played = data.GetInnerText("hasPlayed").Equals("1");
			
			if (Played) {
				BestTime = float.Parse(data.GetInnerText("besttimemilliseconds")) / 1000;
				
				ItemsUsed = new L4D2Items(data);
				
				InfectedKilled = new L4D2Infected(data);
				
				List<SteamID> teammateList = new List<SteamID>();
				foreach (XmlElement teammate in data.GetXmlElement("teammates")) {
					teammateList.Add(SteamID.Create(long.Parse(teammate.InnerText), false));
				}
				Teammates = teammateList.ToArray();
				
				Medal = MedalFrom(data.GetInnerText("medal"));
			}

		}
	}
	
	public class L4D2Explosive : L4DExplosive
	{
		public new static bool IsExplosive(string weaponname)
		{
			return ((weaponname == "molotov") || (weaponname == "pipes") || (weaponname == "bilejars"));
		}
		
		public L4D2Explosive(XmlElement data)
			: base(data)
		{
		}
	}
	
	public class L4D2Weapon : AbstractL4DWeapon
	{ 
		public string Damage      { get; protected set; }
		public string WeaponGroup { get; protected set; }
		
		public L4D2Weapon(XmlElement data)
			: base(data)
		{
			Damage = data.GetInnerText("pctkills");
			
			WeaponGroup = data.Attributes["group"].InnerText;
			
			KillPercentage = ParsePercentage(data.GetInnerText("pctkills"));
		}
	}
	
	public class L4D2Stats : AbstractL4DStats
	{
		
		public L4D2ScavengeStats ScavengeStats { get; protected set; }
		public GameWeapon[] WeaponStats { get; protected set; }
		public L4D2Map[] Maps { get; protected set; }
		
		public float AverageAdrenalineShared  { get; protected set; }
		public float AverageAdrenalineUsed    { get; protected set; }
		public float AverageDefibrillatorUsed { get; protected set; }
		
		public L4D2Stats(string steamid)
			: base(steamid, "l4d2")
		{
			FetchData();
		}
		
		public L4D2Stats(long steamid)
			: base(steamid, "l4d2")
		{
			FetchData();
		}	
		
		protected new void FetchData()
		{
			if (IsPublic) {
				var stats = doc.GetXmlElement("stats");
				
				var lifetime = stats.GetXmlElement("lifetime");
				AverageAdrenalineShared  = float.Parse(lifetime.GetInnerText("adrenalineshared"));
				AverageAdrenalineUsed    = float.Parse(lifetime.GetInnerText("adrenalineused"));
				AverageDefibrillatorUsed = float.Parse(lifetime.GetInnerText("defibrillatorsused"));
				
				var survival = stats.GetXmlElement("survival");
				List <L4D2Map> maps = new List<L4D2Map>();
				foreach (XmlElement map in survival.GetXmlElement("maps")) {
					maps.Add(new L4D2Map(map));
				}
				Maps = maps.ToArray();
			
				ScavengeStats = new L4D2ScavengeStats(stats.GetXmlElement("scavenge"));
				
				var weaponsStats = stats.GetXmlElement("weapons");
				List<GameWeapon> weaponList = new List<GameWeapon>();
				foreach (XmlElement weapon in weaponsStats) {
					// TODO: track this statistics
					if (weapon.Name.EndsWith("PctDmg"))
						continue;
					
					if (L4D2Explosive.IsExplosive(weapon.Name))
						weaponList.Add(new L4D2Explosive(weapon));
					else
						weaponList.Add(new L4D2Weapon(weapon));
				}
				WeaponStats = weaponList.ToArray();
			}
		}
	}
	
	#endregion
}
