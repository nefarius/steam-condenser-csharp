/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;

namespace SteamCondenser.Steam.Community
{
	public class GameAchievement
	{
		public int AppId { get; protected set; }
		public bool Done { get; protected set; }
		public string Name { get; protected set; }
		
		public GameAchievement(int appId, string name, bool done)
		{
			AppId = appId;
			Done = done;
			Name = name;
		}
	}
	
	public abstract class GameClass
	{
		public string Name { get; protected set; }
		public int PlayTime { get; protected set; }
	}
	
	public abstract class GameWeapon 
	{
		public int Kills { get; protected set; }
		public string ID { get; protected set; }
		public int Shots { get; protected set; }
		
		public int AverageShotsPerKill {
			get { 
				if (Kills == 0) return 0;
				return Shots / Kills;
			}
		}
	}
	
	public class SteamCroup 
	{
		
	}
	

}
