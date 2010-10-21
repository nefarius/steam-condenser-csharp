/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2009-2010, Andrius Bentkus
 */

using System;
using System.Threading;
using NUnit.Framework;
using SteamCondenser.Steam.Community;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class Group
	{
		[Test]
		public void MemberCountByCustomUrl()
		{
			SteamGroup.ClearCache();
			SteamGroup group1 = SteamGroup.Create("valve");
			Assert.IsTrue(group1.MemberCount > 0);
			
			// bypass cache
			SteamGroup group2 = SteamGroup.Create("valve", true, true);
			Assert.AreEqual(group1.MemberCount, group2.MemberCount);
			
		}
		
		[Test]
		public void MemberCountBySteamID()
		{
			SteamGroup.ClearCache();
			SteamGroup group1 = SteamGroup.Create(103582791429521412L);
			Assert.IsTrue(group1.MemberCount > 0);
			
			// bypass cache
			SteamGroup group2 = SteamGroup.Create(103582791429521412L, true, true);
			Assert.AreEqual(group1.MemberCount, group2.MemberCount);
		}
		
		[Test]
		public void BypassCache()
		{
			SteamGroup.ClearCache();
			var group1 = SteamGroup.Create("valve", false, false);
			
			Assert.IsFalse(group1.IsFetched);
			group1.FetchMembers();
			Assert.IsTrue(group1.IsFetched);
			
			var group2 = SteamGroup.Create("valve");
			Assert.IsTrue(group2.IsFetched);
			
			Assert.AreNotSame(group1, group2);
			
		}
		
		[Test]
		public void Fetch()
		{
			SteamGroup.ClearCache();
			var grp = SteamGroup.Create("valve", false);
			Assert.IsFalse(grp.IsFetched);
			Assert.IsTrue(grp.FetchMembers());
			Assert.IsTrue(grp.IsFetched);
			
			SteamGroup.ClearCache();
			grp = SteamGroup.Create("valve", true);
			Assert.IsTrue(grp.IsFetched);
			
			SteamGroup.ClearCache();
			grp = SteamGroup.Create("valve");
			Assert.IsTrue(grp.IsFetched);
			
		}
		
		[Test]
		public void CacheCustomUrl()
		{
			
			SteamGroup.ClearCache();
			var group1 = SteamGroup.Create("valve", false, false);

			Assert.IsFalse(SteamGroup.IsCached("valve"));
			Assert.IsFalse(SteamGroup.IsCached(103582791429521412L));

			group1.Cache();
			
			Assert.IsTrue(SteamGroup.IsCached("valve"));
			Assert.IsFalse(SteamGroup.IsCached(103582791429521412L));
			
			group1.FetchMembers();
			group1.Cache();
	
			Assert.IsTrue(SteamGroup.IsCached("valve"));
			Assert.IsTrue(SteamGroup.IsCached(103582791429521412L));
			
			var group2 = SteamGroup.Create("valve", false);
			Assert.AreSame(group1, group2);
			
			var group3 = SteamGroup.Create(103582791429521412L, false);
			Assert.AreSame(group1, group3);
			
			
			SteamGroup.ClearCache();
			group1 = SteamGroup.Create("valve");
			
			Assert.IsTrue(SteamGroup.IsCached("valve"));
			Assert.IsTrue(SteamGroup.IsCached(103582791429521412L));
			
		}
		
		[Test]
		public void CacheGroupID()
		{
			SteamGroup.ClearCache();
			var group1 = SteamGroup.Create(103582791429521412L, false, false);
			
			Assert.IsFalse(SteamGroup.IsCached("valve"));
			Assert.IsFalse(SteamGroup.IsCached(103582791429521412L));
			
			group1.Cache();
			
			Assert.IsFalse(SteamGroup.IsCached("valve"));
			Assert.IsTrue(SteamGroup.IsCached(103582791429521412L));
			
			group1.FetchMembers();
			group1.Cache();
	
			Assert.IsFalse(SteamGroup.IsCached("valve"));
			Assert.IsTrue(SteamGroup.IsCached(103582791429521412L));
			
			var group2 = SteamGroup.Create("valve");
			Assert.AreNotSame(group1, group2);
			
			var group3 = SteamGroup.Create(103582791429521412L);
			Assert.AreSame(group1, group3);
		}
		
		[Test]
		public void CaseInsensitivity()
		{
			SteamGroup.ClearCache();
			var group1 = SteamGroup.Create("valve");
			
			Assert.IsTrue(SteamGroup.IsCached("valve"));
			Assert.IsTrue(SteamGroup.IsCached("Valve"));
			Assert.IsTrue(SteamGroup.IsCached("VALVE"));
			
			var group2 = SteamGroup.Create("Valve");
			var group3 = SteamGroup.Create("VALVE");
			
			Assert.AreEqual(group1, group2);
			Assert.AreEqual(group1, group3);
		}
	}
	
}