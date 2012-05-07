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
	public class DoDSWeapon : GameWeapon
	{
		public string Name { get; protected set; }
		
		public int Headshots { get; protected set; }
		public int Hits      { get; protected set; }
		
		public DoDSWeapon(XmlElement data)
			: base(data)
		{
			ID = data.GetAttribute("key");
			
			Name      =           data.GetInnerText("name");
			Headshots = int.Parse(data.GetInnerText("headshots"));
			Shots     = int.Parse(data.GetInnerText("shotsfired"));
			Hits      = int.Parse(data.GetInnerText("shotshit"));
			
		}
		
		public float AverageHitsPerKill {
			get {
				if (Hits == 0) return 0.0f;
				return (float)Hits / Kills;
			}
		}
		
		public float HeadshotPercentage {
			get {
				if (Hits == 0) return 0.0f;
				return (float)Headshots/Hits;
			}
		}
		
	}
	
	public class DoDSClass : GameClass
	{
		public string Key { get; protected set; }
		
		public int Blocks       { get; protected set; }
		public int BombsDefused { get; protected set; }
		public int BombsPlanted { get; protected set; }
		public int Captures     { get; protected set; }
		public int Deaths       { get; protected set; }
		public int Dominations  { get; protected set; }
		public int Kills        { get; protected set; }
		public int RoundsLost   { get; protected set; }
		public int RoundsWon    { get; protected set; }
		public int Revenges     { get; protected set; }
		
		public DoDSClass(XmlElement data)
		{
			Key = data.GetAttribute("key");
			
			Name = data.GetInnerText("name");
			
			Blocks       = int.Parse(data.GetInnerText("blocks"));
			BombsDefused = int.Parse(data.GetInnerText("bombsdefused"));
			BombsPlanted = int.Parse(data.GetInnerText("bombsplanted"));
			Captures     = int.Parse(data.GetInnerText("captures"));
			Deaths       = int.Parse(data.GetInnerText("deaths"));
			Dominations  = int.Parse(data.GetInnerText("dominations"));
			Kills        = int.Parse(data.GetInnerText("kills"));
			RoundsLost   = int.Parse(data.GetInnerText("roundslost"));
			RoundsWon    = int.Parse(data.GetInnerText("roundswon"));
			RoundsLost   = int.Parse(data.GetInnerText("roundslost"));
			Revenges     = int.Parse(data.GetInnerText("revenges"));
			
			PlayTime = int.Parse(data.GetInnerText("playtime"));
		}
		
	}
	
	public class DoDSStats : GameStats
	{
		public DoDSClass[]  ClassStats { get; protected set; }
		public DoDSWeapon[] WeaponStats { get; protected set; }
			
		public DoDSStats(long steamid)
			: base(steamid, "dod:s")
		{
			FetchData();
		}

		public DoDSStats(string steamid)
			: base(steamid, "dod:s")
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			if (IsPublic)
			{
				List<DoDSClass> classList = new List<DoDSClass>();
				foreach (XmlElement klass in doc.GetXmlElement("classes").GetElementsByTagName("class")) {
					classList.Add(new DoDSClass(klass));
				}
				ClassStats = classList.ToArray();
				
				List<DoDSWeapon> weaponList = new List<DoDSWeapon>();
				foreach (XmlElement weapon in doc.GetXmlElement("weapons")) {
					weaponList.Add(new DoDSWeapon(weapon));
				}
				WeaponStats = weaponList.ToArray();
			}
		}
	}
}

