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
	#region Character Class classes

	public class TF2Class : GameClass
	{
		public int MaxBuildingsDestroyed { get; protected set; }
		public int MaxCaptures           { get; protected set; }
		public int MaxDamage             { get; protected set; }
		public int MaxDefenses           { get; protected set; }
		public int MaxDominations        { get; protected set; }
		public int MaxKillAssists        { get; protected set; }
		public int MaxKills              { get; protected set; }
		public int MaxRevenges           { get; protected set; }
		public int MaxScore              { get; protected set; }
		public int MaxTimeAlive          { get; protected set; }

		public TF2Class(XmlElement data)
			: base(data)
		{
			MaxBuildingsDestroyed = int.Parse(data.GetInnerText("ibuildingsdestroyed"));
			MaxCaptures           = int.Parse(data.GetInnerText("ipointcaptures"));
			MaxDamage             = int.Parse(data.GetInnerText("idamagedealt"));
			MaxDefenses           = int.Parse(data.GetInnerText("ipointdefenses"));
			MaxDominations        = int.Parse(data.GetInnerText("idominations"));
			MaxKillAssists        = int.Parse(data.GetInnerText("ikillassists"));
			MaxKills              = int.Parse(data.GetInnerText("inumberofkills"));
			MaxRevenges           = int.Parse(data.GetInnerText("irevenge"));
			MaxScore              = int.Parse(data.GetInnerText("ipointsscored"));
			MaxTimeAlive          = int.Parse(data.GetInnerText("iplaytime"));

		}

		/// <summary>
		/// Factory method
		/// </summary>
		public static TF2Class Create(XmlElement data)
		{
			switch (data.GetInnerText("className"))
			{
			case "Engineer":
				return new TF2Engineer(data);
			case "Medic":
				return new TF2Medic(data);
			case "Sniper":
				return new TF2Sniper(data);
			case "Spy":
				return new TF2Spy(data);
			default:
				return new TF2Class(data);
			}
		}
	}

	public class TF2Engineer : TF2Class
	{
		public int MaxBuildingsBuild { get; protected set; }
		public int MaxSentryKills    { get; protected set; }
		public int MaxTeleports      { get; protected set; }

		public TF2Engineer(XmlElement data)
			: base(data)
		{
			MaxBuildingsBuild = int.Parse(data.GetInnerText("ibuildingsbuilt"));
			MaxSentryKills    = int.Parse(data.GetInnerText("isentrykills"));
			MaxTeleports      = int.Parse(data.GetInnerText("inumteleports"));
		}
	}

	public class TF2Medic : TF2Class
	{
		public int MaxHealthHealed { get; protected set; }
		public int MaxUeberCharges { get; protected set; }

		public TF2Medic(XmlElement data)
			: base(data)
		{
			MaxHealthHealed = int.Parse(data.GetInnerText("ihealthpointshealed"));
			MaxUeberCharges = int.Parse(data.GetInnerText("inuminvulnerable"));
		}
	}

	public class TF2Sniper : TF2Class
	{
		public int MaxHeadShots { get; protected set; }

		public TF2Sniper(XmlElement data)
			: base(data)
		{
			MaxHeadShots = int.Parse(data.GetInnerText("iheadshots"));
		}
	}

	public class TF2Spy : TF2Class
	{
		public int MaxBackstabs     { get; protected set; }
		public int MaxHealthLeeched { get; protected set; }

		public TF2Spy(XmlElement data)
			: base(data)
		{
			MaxBackstabs     = int.Parse(data.GetInnerText("ibackstabs"));
			MaxHealthLeeched = int.Parse(data.GetInnerText("ihealthpointsleached"));
		}
	}

	#endregion

	public class TF2Stats : GameStats
	{
		public const string AppName = "tf2";

		public TF2Class[] ClassStats { get; protected set; }

		public TF2Stats(string steamid)
			: base(steamid, AppName)
		{
			FetchData();
		}

		public TF2Stats(long steamid)
			: base(steamid, AppName)
		{
			FetchData();
		}

		protected new void FetchData()
		{
			var classes = (doc.GetElementsByTagName("stats").Item(0) as XmlElement).GetElementsByTagName("classData");

			List<TF2Class> list = new List<TF2Class>();
			foreach (XmlElement klass in classes) {
				list.Add(TF2Class.Create(klass));
			}
			ClassStats = list.ToArray();

		}
	}
}
