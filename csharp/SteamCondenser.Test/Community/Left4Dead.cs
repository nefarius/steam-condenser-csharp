/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2009-2010, Andrius Bentkus
 */

using System;
using NUnit.Framework;
using SteamCondenser.Steam.Community;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class Left4Dead
	{
		[Test]
		public void CustomUrl()
		{
			L4DStats stats = new L4DStats("toxedvirus");
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
						
			Assert.Greater(stats.Achievments.Length, 0);
			
			stats = (SteamID.Create("toxedvirus")).L4DStats;
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Achievments.Length, 0);
		}
		
		[Test]
		public void SteamID64()
		{
			L4DStats stats = new L4DStats(76561197989711328);
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Achievments.Length, 0);
			
			stats = SteamID.Create(76561197989711328).L4DStats;
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
			Assert.Greater(stats.Achievments.Length, 0);
		}
	}
}

