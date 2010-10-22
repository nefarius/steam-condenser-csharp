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
	public class CounterStrikeSource
	{
		[Test]
		public void CustomUrl()
		{
			CSSStats stats = new CSSStats("koraktor");
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
						
//			Assert.Greater(stats.ClassStats.Length, 0);
//			Assert.AreEqual(stats.ClassStats.Length, 9);
			
			Assert.Greater(stats.Achievments.Length, 0);
		}
		
		[Test]
		public void SteamID64()
		{
			CSSStats stats = new CSSStats(76561197961384956);
			
			Assert.AreNotEqual(stats.AppID, 0);
			Assert.Greater(stats.AppID, 0);
						
//			Assert.Greater(stats.ClassStats.Length, 0);
//			Assert.AreEqual(stats.ClassStats.Length, 9);
			
			Assert.Greater(stats.Achievments.Length, 0);
		}		
	}
}

