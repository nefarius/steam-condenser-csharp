/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam
{
	public class SteamPlayer
	{
		public int    ID    { get; protected set; }
		public String Name  { get; protected set; }
		public int    Score { get; protected set; }
		public bool   IsBot { get; protected set; }
		
		public TimeSpan ConnectTime { get; protected set; }

		public SteamPlayer(int id, String name, int score, float connectTime)
		{
			ID    = id;
			Name  = name;
			Score = score;

			if (connectTime == -1) {
				IsBot = true;
				ConnectTime = TimeSpan.FromSeconds(0);
			} else {
				IsBot = false;
				ConnectTime = TimeSpan.FromSeconds((double)Math.Round((double)connectTime));
			}
		}

		public override string ToString()
		{
			return "#" + ID + " \"" + Name + "\" " + Score + " " + ConnectTime;
		}
	}
}
