/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using NUnit.Framework;
using SteamCondenser.Steam.Community;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class AlienSwarm
	{
		[Test]
		public void CustomUrl()
		{
			AlienSwarmStats stats = new AlienSwarmStats("toxedvirus");
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Missions.Length, 0);
			Assert.Greater(stats.Weapons.Length, 0);
			
			stats = (SteamID.Create("toxedvirus")).AlienSwarmStats;
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Missions.Length, 0);
			Assert.Greater(stats.Weapons.Length, 0);
		}
		
		[Test]
		public void SteamID64()
		{
			AlienSwarmStats stats = new AlienSwarmStats(76561197989711328);
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Missions.Length, 0);
			Assert.Greater(stats.Weapons.Length, 0);
			
			stats = SteamID.Create(76561197989711328).AlienSwarmStats;
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Missions.Length, 0);
			Assert.Greater(stats.Weapons.Length, 0);
		}
	}
}
