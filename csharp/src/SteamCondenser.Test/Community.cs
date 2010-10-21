/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2009-2010, Andrius Bentkus
 */

using System;
using SteamCondenser.Steam.Community;
using NUnit.Framework;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class Community
	{
		[Test]
		public void GroupByCustomUrl()
		{
			SteamGroup grp = SteamGroup.Create("valve");
			Console.WriteLine(grp.MemberCount);
			
		}
		
		[Test]
		public void GroupByGroupID64()
		{
			SteamGroup grp = SteamGroup.Create(103582791429521412L);
			Console.WriteLine (grp.MemberCount);
		}
	}
}

