// /**
//  * This code is free software; you can redistribute it and/or modify it under
//  * the terms of the new BSD License.
//  *
//  * Copyright (c) 2009-2010, Andrius Bentkus
//  */
// 

using System;
using SteamCondenser.Steam.Servers;
using NUnit.Framework;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class QueryTests
	{
		[Test]
		[ExpectedException(typeof(System.Net.Sockets.SocketException))]
		public void InvalidGoldSrcServer()
		{
			GoldSrcServer server = new GoldSrcServer("10.0.0.1");
			int ping = server.Ping;
			Assert.Fail("Exception should have been thrown");
		}
		
		[Test]
		[ExpectedException(typeof(System.Net.Sockets.SocketException))]
		public void InvalidSourceServer()
		{
			SourceServer server = new SourceServer("10.0.0.1");
			int ping = server.Ping;
			Assert.Fail("Exception should have been thrown");
		}
		
		[Test]
		public void RandomGoldSrcServer()
		{
			Random randomizer = new Random();
			MasterServer masterServer = new MasterServer(MasterServer.GoldSrcMasterServer);
			var servers = masterServer.GetServers();
			var point   = servers[randomizer.Next(servers.Count)];
			
			GoldSrcServer server = new GoldSrcServer(point);
			server.UpdatePlayerInfo();
			server.UpdateRulesInfo();
		}
		
		[Test]
		public void RandomSourceServeR()
		{
			Random randomizer = new Random();
			MasterServer masterServer = new MasterServer(MasterServer.SourceMasterServer);
			var servers = masterServer.GetServers();
			var point   = servers[randomizer.Next(servers.Count)];
			
			GoldSrcServer server = new GoldSrcServer(point);
			server.UpdatePlayerInfo();
			server.UpdateRulesInfo();
		}
	}
}

