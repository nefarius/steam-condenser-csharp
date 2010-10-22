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
	public class ID
	{
		[Test]
		public void Converting()
		{
			Assert.AreEqual(SteamID.ConvertSteamIDToCommunityID("STEAM_0:0:14722800"),
			                76561197989711328L);
			
			Assert.AreEqual(SteamID.ConvertCommunityIDToSteamID(76561197989711328),
			                "STEAM_0:0:14722800");
		}
	}
}