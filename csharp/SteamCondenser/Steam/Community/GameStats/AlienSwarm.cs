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
	
	public class AlienSwarmMissionTimes
	{
		public string Average { get; set; }
		public string Total   { get; set; }
		
		public string Easy   { get; set; }
		public string Normal { get; set; }
		public string Hard   { get; set; }
		public string Brutal { get; set; }
		public string Insane { get; set; }
		
		public AlienSwarmMissionTimes(XmlElement data)	
		{
			Average = data.GetInnerText("avgtime");
			Total   = data.GetInnerText("totaltime");
			
			Easy   = data.GetInnerText("easytime");
			Normal = data.GetInnerText("normaltime");
			Hard   = data.GetInnerText("hardtime");
			Brutal = data.GetInnerText("brutaltime");
			Insane = data.GetInnerText("insanetime");
		}
	}
		
	public class AlienSwarmMission
	{
		public AlienSwarmMissionTimes MissionTimes { get; protected set; }
		
		public float AverageDamageTaken  { get; protected set; }
		public float AverageFriendlyFire { get; protected set; }
		public float AverageKills        { get; protected set; }
		
		public string BestDifficulty  { get; protected set; }
		public int    DamageTaken     { get; protected set; }
		public int    FriendlyFire    { get; protected set; }
		public int    GamesSuccessful { get; protected set; }
		
		public string Image   { get; protected set; }
		public int    Kills   { get; protected set; }
		public string MapName { get; protected set; }
		public string Name    { get; protected set; }
		
		public int   TotalGames           { get; protected set; }
		public float TotalGamesPercentage { get; protected set; }
		
		public AlienSwarmMission(XmlElement data)
		{
			AverageDamageTaken  = float.Parse(data.GetInnerText("damagetakenavg"));
			AverageFriendlyFire = float.Parse(data.GetInnerText("friendlyfireavg"));
			AverageKills        = float.Parse(data.GetInnerText("killsavg"));
			
			BestDifficulty =            data.GetInnerText("bestdifficulty");
			DamageTaken    =  int.Parse(data.GetInnerText("damagetaken"));
			FriendlyFire   =  int.Parse(data.GetInnerText("friendlyfire"));
			GamesSuccessful = int.Parse(data.GetInnerText("gamessuccess"));
			
			Image = AlienSwarmStats.AppUrl + data.GetInnerText("image");
			
			Kills   = int.Parse(data.GetInnerText("kills"));
			MapName = /* get node name */ "";
			Name    =           data.GetInnerText("name");
			
			TotalGames           =   int.Parse(data.GetInnerText("gamestotal"));
			TotalGamesPercentage = float.Parse(data.GetInnerText("gamestotalpct"));
			
			MissionTimes = new AlienSwarmMissionTimes(data);
		}
		
	}
	
	public class AlienSwarmWeapon : GameWeapon
	{
		
		public float  Accuracy     { get; protected set; }
		public int    Damage       { get; protected set; }
		public int    FriendlyFire { get; protected set; }
		public string Name         { get; protected set; }
		
		public AlienSwarmWeapon(XmlElement data)
			: base(data)
		{
			Accuracy     = float.Parse(data.GetInnerText("accuracy"));
			Damage       =   int.Parse(data.GetInnerText("damage"));
			FriendlyFire =   int.Parse(data.GetInnerText("friendlyfire"));
			Name         =             data.GetInnerText("name");
			Shots        =   int.Parse(data.GetInnerText("shotsfired"));
		}
	}
	
	public class AlienSwarmWeaponStats {
		
		public int AmmoDeployed          { get; protected set; }
		public int SentryGunsDeployed    { get; protected set; }
		public int SentryFlamersDeployed { get; protected set; }
		public int SentryFreezeDeployed  { get; protected set; }
		public int SentryCannonDeployed  { get; protected set; }
		public int MedkitsUsed           { get; protected set; }
		public int FlaresUsed            { get; protected set; }
		public int AdrenalineUsed        { get; protected set; }
		public int TeslaTrapsDeployed    { get; protected set; }
		public int FreezeGrenadesThrown  { get; protected set; }
		public int ElectricArmorUsed     { get; protected set; }
		public int HealgunHeals          { get; protected set; }
		public int HealgunHealsSelf      { get; protected set; }
		public int HealbeaconHeals       { get; protected set; }
		public int HealbeaconHealsSelf   { get; protected set; }
		public int DamageAmpsUsed        { get; protected set; }
		public int HealbeaconsDeployed   { get; protected set; }
		
		public float HealgunHealsPercentage        { get; protected set; }
		public float HealgunHealsSelfPercentage    { get; protected set; }
		public float HealbeaconHealsPercentage     { get; protected set; }
		public float HealbeaconHealsSelfPercentage { get; protected set; }
		
		
		public AlienSwarmWeaponStats(XmlElement data)
		{
			AmmoDeployed          = int.Parse(data.GetInnerText("ammo_deployed"));
			SentryGunsDeployed    = int.Parse(data.GetInnerText("sentryguns_deployed"));
			SentryFlamersDeployed = int.Parse(data.GetInnerText("sentry_flamers_deployed"));
			SentryFreezeDeployed  = int.Parse(data.GetInnerText("sentry_freeze_deployed"));
			SentryCannonDeployed  = int.Parse(data.GetInnerText("sentry_cannon_deployed"));
			MedkitsUsed           = int.Parse(data.GetInnerText("medkits_used"));
			FlaresUsed            = int.Parse(data.GetInnerText("flares_used"));
			AdrenalineUsed        = int.Parse(data.GetInnerText("adrenaline_used"));
			TeslaTrapsDeployed    = int.Parse(data.GetInnerText("tesla_traps_deployed"));
			FreezeGrenadesThrown  = int.Parse(data.GetInnerText("freeze_grenades_thrown"));
			ElectricArmorUsed     = int.Parse(data.GetInnerText("electric_armor_used"));
			HealgunHeals          = int.Parse(data.GetInnerText("healgun_heals"));
			HealgunHealsSelf      = int.Parse(data.GetInnerText("healgun_heals_self"));
			HealbeaconHeals       = int.Parse(data.GetInnerText("healbeacon_heals"));
			HealbeaconHealsSelf   = int.Parse(data.GetInnerText("healbeacon_heals_self"));
			DamageAmpsUsed        = int.Parse(data.GetInnerText("damage_amps_used"));
			HealbeaconsDeployed   = int.Parse(data.GetInnerText("healbeacons_deployed"));
			
			
			HealgunHealsPercentage        = float.Parse(data.GetInnerText("healgun_heals_pct"));
			HealgunHealsSelfPercentage    = float.Parse(data.GetInnerText("healbeacon_heals_pct_self"));
			HealbeaconHealsPercentage     = float.Parse(data.GetInnerText("healbeacon_heals_pct"));
			HealbeaconHealsSelfPercentage = float.Parse(data.GetInnerText("healbeacon_heals_pct_self"));
		}
	}
	
	public class AlienSwarmFavorites 
	{
		public string Class           { get; set; }
		public string ClassImage      { get; set; }
		public float  ClassPercentage { get; set; }
		
		public string Difficulty           { get; set; }
		public float  DifficultyPercentage { get; set; }
		
		public string Extra           { get; set; }
		public string ExtraImage      { get; set; }
		public float  ExtraPercentage { get; set; }
		
		public string Marine           { get; set; }
		public string MarineImage      { get; set; }
		public float  MarinePercentage { get; set; }
		
		public string Mission           { get; set; }
		public string MissionImage      { get; set; }
		public float  MissionPercentage { get; set; }
		
		public string PrimaryWeapon           { get; set; }
		public string PrimaryWeaponImage      { get; set; }
		public float  PrimaryWeaponPercentage { get; set; }
		
		public string SecondaryWeapon           { get; set; }
		public string SecondaryWeaponImage      { get; set; }
		public float  SecondaryWeaponPercentage { get; set; }
		
		
		public AlienSwarmFavorites(XmlElement data)
		{
			Class           =             data.GetInnerText("class");
			ClassImage      =             data.GetInnerText("classimg");
			ClassPercentage = float.Parse(data.GetInnerText("classpct"));
			
			Difficulty           =             data.GetInnerText("difficulty");
			DifficultyPercentage = float.Parse(data.GetInnerText("difficultypct"));
			
			Extra           =             data.GetInnerText("extra");
			ExtraImage      =             data.GetInnerText("extraimg");
			ExtraPercentage = float.Parse(data.GetInnerText("extrapct"));
			
			Marine           =             data.GetInnerText("marine");
			MarineImage      =             data.GetInnerText("marineimg");
			MarinePercentage = float.Parse(data.GetInnerText("marinepct"));
			
			Mission           =             data.GetInnerText("mission");
			MissionImage      =             data.GetInnerText("missionimg");
			MissionPercentage = float.Parse(data.GetInnerText("missionpct"));
			
			PrimaryWeapon           =             data.GetInnerText("primary");
			PrimaryWeaponImage      =             data.GetInnerText("primaryimg");
			PrimaryWeaponPercentage = float.Parse(data.GetInnerText("primarypct"));
			
			SecondaryWeapon           =             data.GetInnerText("secondary");
			SecondaryWeaponImage      =             data.GetInnerText("secondaryimg");
			SecondaryWeaponPercentage = float.Parse(data.GetInnerText("secondarypct"));
		}
	}
	
	public class AlienSwarmStats : GameStats
	{
		public const string AppName = "alienswarm";
		
		private static string[] weapons = { 
			"Autogun", "Cannon_Sentry", "Chainsaw", "Flamer", 
			"Grenade_Launcher", "Hand_Grenades", "Hornet_Barrage",
			"Incendiary_Sentry", "Laser_Mines", "Marskman_Rifle", "Minigun",
			"Mining_Laser", "PDW", "Pistol", "Prototype_Rifle", "Rail_Rifle",
			"Rifle", "Rifle_Grenade", "Sentry_Gun", "Shotgun", "Tesla_Cannon",
			"Vindicator", "Vindicator_Grenade" 
		};
		
		public AlienSwarmWeaponStats  WeaponStats  { get; protected set; }
		public AlienSwarmWeapon[]     Weapons      { get; protected set; }
		public AlienSwarmFavorites    Favorites    { get; protected set; }
		public AlienSwarmMission[]    Missions     { get; protected set; }
		
		public float  Accuracy           { get; protected set; }
		public int    AliensBurned       { get; protected set; }
		public int    AliensKilled       { get; protected set; }
		public int    Campaigns          { get; protected set; }
		public int    DamageTaken        { get; protected set; }
		public int    Experience         { get; protected set; }
		public int    ExperienceRequired { get; protected set; }
		public int    FastHacks          { get; protected set; }
		public int    FriendlyFire       { get; protected set; }
		public int    GamesSuccessful    { get; protected set; }
		public int    Healing            { get; protected set; }
		public float  KillsPerHour       { get; protected set; }
		public int    Level              { get; protected set; }
		public int    Promotion          { get; protected set; }
		public string PromotionImage     { get; protected set; }
		public string NextUnlock         { get; protected set; }
		public string NextUnlockImage    { get; protected set; }
		public int    ShotsFired         { get; protected set; }
		public int    TotalGames         { get; protected set; }
		
		public float GamesSusscessfulPercentage { get; protected set; }
		
		public AlienSwarmStats(string steamId)
			: base(steamId, AppName)
		{
			FetchData();
		}
		
		public AlienSwarmStats(long steamId)
			: base(steamId, AppName)
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			if (IsPublic)
			{
				var stats = doc.GetXmlElement("stats");
				
				Accuracy           = float.Parse(stats.GetInnerText("accuracy"));
				AliensBurned       =   int.Parse(stats.GetInnerText("aliensburned"));
				AliensKilled       =   int.Parse(stats.GetInnerText("alienskilled"));
				Campaigns          =   int.Parse(stats.GetInnerText("campaigns"));
				DamageTaken        =   int.Parse(stats.GetInnerText("damagetaken"));
				Experience         =   int.Parse(stats.GetInnerText("experience"));
				ExperienceRequired =   int.Parse(stats.GetInnerText("xprequired"));
				FastHacks          =   int.Parse(stats.GetInnerText("fasthacks"));
				GamesSuccessful    =   int.Parse(stats.GetInnerText("gamessuccess"));
				Healing            =   int.Parse(stats.GetInnerText("healing"));
				KillsPerHour       = float.Parse(stats.GetInnerText("killsperhour"));
				Level              =   int.Parse(stats.GetInnerText("level"));
				Promotion          =   int.Parse(stats.GetInnerText("promotion"));
				NextUnlock         =             stats.GetInnerText("nextunlock");
				NextUnlockImage    =             stats.GetInnerText("nextunlockimg");
				TotalGames         =   int.Parse(stats.GetInnerText("totalgames"));
				
				if (Promotion > 0)
					PromotionImage = "http://steamcommunity.com/public/images/gamestats/swarm/" + stats.GetInnerText("promotionpic");
				
				GamesSusscessfulPercentage = (TotalGames > 0 ? (float)GamesSuccessful / TotalGames : 0);
				
				WeaponStats = new AlienSwarmWeaponStats(stats.GetXmlElement("weapons"));
				
				var xmlweapons = stats.GetXmlElement("weapons");
				List<AlienSwarmWeapon> weaponlist = new List<AlienSwarmWeapon>();
				foreach (string weapon in weapons)
				{
					weaponlist.Add(new AlienSwarmWeapon(xmlweapons.GetXmlElement(weapon)));
				}
				Weapons = weaponlist.ToArray();
				
				Favorites = new AlienSwarmFavorites(stats.GetXmlElement("favorites"));
				
				List<AlienSwarmMission> missionlist = new List<AlienSwarmMission>();
				foreach (XmlElement mission in stats.GetXmlElement("missions"))
				{
					if (mission.NodeType == XmlNodeType.Text)
						continue;
					missionlist.Add(new AlienSwarmMission(mission));
				}
				
				Missions = missionlist.ToArray();
			}
		}
		
	}
	
}