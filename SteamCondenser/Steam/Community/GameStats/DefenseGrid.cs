/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Xml;
using System.Collections.Generic;
using SteamCondenser.Steam.Community;

namespace SteamCondenser.Steam.Community
{
	public class DefenseGridAlien 
	{
		private static string[] alienClasses = new string[] { 
			"bulwark", "crasher", "dart", "decoy", "drone", "grunt", "juggernaut", "manta",
			"racer", "rumbler", "seeker", "spire", "stealth", "swarmer", "turtle", "walker" 
		};
		
		public static string[] AlienClasses { get { return alienClasses; }}
		
		public static bool IsAlienClass(string name)
		{
			foreach (string alienname in AlienClasses) {
				if (alienname == name) return true;
			}
			return false;
		}
		
		public string Name { get; protected set; }
		
		public int Encountered { get; protected set; }
		public int Killed      { get; protected set; }
		
		public DefenseGridAlien(XmlElement data)
		{
			Name = data.Name;
			
			Encountered = int.Parse(data.GetValueText("encountered"));
			Killed      = int.Parse(data.GetValueText("killed"));
		}
	}
	
	public class DefenseGridTower 
	{
		private static string[] towerClasses = 
			new string[] { "cannon", "flak", "gun", "inferno", "laser", "meteor", "missile", "tesla" };
		
		public static string[] TowerClasses { get { return towerClasses; } }
		
		public static bool IsTowerClass(string name)
		{
			foreach (string towername in TowerClasses) {
				if (towername == name)
					return true;
			}
			return false;
		}
		
		public string Class  { get; protected set; }
		public int    Level  { get; protected set; }
		public int    Built  { get; protected set; }
		public bool   HasDamage { get; protected set; }
		public float  Damage { get; protected set; }
		
		public DefenseGridTower(XmlElement data)
		{
			Class = data.Name;
			Level = int.Parse(data.GetAttribute("level"));
			
			Built  =   int.Parse(data.GetValueText("built"));
			if (data.GetXmlElement("damage") != null) {
				HasDamage = true;
				Damage = float.Parse(data.GetValueText("damage"));
			}
		}
	}
	
	public class DefenseGridCommand
	{
		public int Level { get; protected set; }
		public int Built { get; protected set; }
		public float Resources { get; protected set; }
		
		public DefenseGridCommand(XmlElement data)
		{
			Level = int.Parse(data.GetAttribute("level"));
			
			Built     =   int.Parse(data.GetValueText("built"));
			Resources = float.Parse(data.GetValueText("resource"));
		}
	}
	
	public class DefenseGridStats : GameStats
	{
		public int BronzeMedals { get; protected set; }
		public int SilverMedals { get; protected set; }
		public int GoldMedals   { get; protected set; }
		
		public int LevelsPlayed          { get; protected set; }
		public int LevelsPlayedCampaign  { get; protected set; }
		public int LevelsPlayedChallenge { get; protected set; }
		public int LevelsWon             { get; protected set; }
		public int LevelsWonCampaign     { get; protected set; }
		public int LevelsWonChallenge    { get; protected set; }
		
		public int Encountered     { get; protected set; }
		public int Killed          { get; protected set; }
		public int KilledCampaign  { get; protected set; }
		public int KilledChallenge { get; protected set; }
		
		public float Resources          { get; protected set; }
		public float HeatDamage         { get; protected set; }
		public float TimePlayed         { get; protected set; }
		public float Interest           { get; protected set; }
		public float Damage             { get; protected set; }
		public float DamageCampaign     { get; protected set; }
		public float DamageChallenge    { get; protected set; }
		public int   OrbitalLaserFired  { get; protected set; }
		public float OrbitalLaserDamage { get; protected set; }
		
		public DefenseGridAlien[] Aliens { get; protected set; }
		public DefenseGridTower[] Towers { get; protected set; }
		public DefenseGridCommand[] Commands { get; protected set; }
		
		public DefenseGridStats(string steamid)
			: base(steamid, "defensegrid:awakening")
		{
			FetchData();
		}
		
		public DefenseGridStats(long steamid)
			: base(steamid, "defensegrid:awakening")
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			if (IsPublic) {
				try {
					var stats = doc.GetXmlElement("stats");
					var general = stats.GetXmlElement("general");
					
					BronzeMedals = int.Parse(general.GetValueText("bronze_medals_won"));
					SilverMedals = int.Parse(general.GetValueText("silver_medals_won"));
					GoldMedals   = int.Parse(general.GetValueText("gold_medals_won"));
					
					LevelsPlayed          = int.Parse(general.GetValueText("levels_played_total"));
					LevelsPlayedCampaign  = int.Parse(general.GetValueText("levels_played_total"));
					LevelsPlayedChallenge = int.Parse(general.GetValueText("levels_played_challenge"));
					LevelsWon             = int.Parse(general.GetValueText("levels_won_total"));
					LevelsWonCampaign     = int.Parse(general.GetValueText("levels_won_campaign"));
					LevelsWonChallenge    = int.Parse(general.GetValueText("levels_won_challenge"));
					
					Encountered     = int.Parse(general.GetValueText("total_aliens_encountered"));
					Killed          = int.Parse(general.GetValueText("total_aliens_killed"));
					KilledCampaign  = int.Parse(general.GetValueText("total_aliens_killed_campaign"));
					KilledChallenge = int.Parse(general.GetValueText("total_aliens_killed_challenge"));
					
					Resources  = float.Parse(general.GetValueText("resources_recovered"));
					HeatDamage = float.Parse(general.GetValueText("heatdamage"));
					TimePlayed = float.Parse(general.GetValueText("time_played"));
					Interest   = float.Parse(general.GetValueText("interest_gained"));
					Damage     = float.Parse(general.GetValueText("tower_damage_total"));
					
					DamageCampaign  = float.Parse(general.GetValueText("tower_damage_total_campaign"));
					DamageChallenge = float.Parse(general.GetValueText("tower_damage_total_challenge"));
					
					var orbital = stats.GetXmlElement("orbitallaser");
					OrbitalLaserFired  =   int.Parse(orbital.GetValueText("fired"));
					OrbitalLaserDamage = float.Parse(orbital.GetValueText("damage"));
					
					var aliens = stats.GetXmlElement("aliens");
					List<DefenseGridAlien> alienList = new List<DefenseGridAlien>();
					foreach (XmlElement alien in aliens) {
						alienList.Add(new DefenseGridAlien(alien));
					}
					Aliens = alienList.ToArray();
					
					var towers = stats.GetXmlElement("towers");
					List<DefenseGridTower> towerList = new List<DefenseGridTower>();
					List<DefenseGridCommand> commandList = new List<DefenseGridCommand>();
					foreach (XmlElement tower in towers) {
						if (tower.Name == "command")
							commandList.Add(new DefenseGridCommand(tower));
					    else
							towerList.Add(new DefenseGridTower(tower));
					}
					Towers = towerList.ToArray();
					Commands = commandList.ToArray();
				
				} catch (XmlException) {
					throw new SteamCondenserException("Stats could not be parsed.");
				}
			}
		}
	}
}

