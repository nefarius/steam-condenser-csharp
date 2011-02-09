/**
* This code is free software; you can redistribute it and/or modify it under
* the terms of the new BSD License.
*
* Copyright (c) 2009-2010, Andrius Bentkus
*/ 

using System;
using System.IO;
using SteamCondenser.Steam.Servers;
using NUnit.Framework;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class RCONTests
	{
		string goldSrcServerAddress = "127.0.0.1";
		int    goldSrcServerPort    = 27015;
		string goldSrcServerAuth    = "test";
		int    goldSrcServerTimeout = 1000;
		
		string sourceServerAddress  = "127.0.0.1";
		int    sourceServerPort     = 27015;
		string sourceServerAuth     = "test";
		int    sourceServerTimeout  = 1000;
		
		/// <summary>
		/// Setup method to load fixtures.
		/// </summary>
		
		[TestFixtureSetUp]
		public void Init()
		{
			try {
				Properties properties = new Properties();
				properties.Load(new FileStream("test-fixtures.properties", FileMode.Open));
				
				if (properties.ContainsKey("goldSrcServerAddress")) goldSrcServerAddress =           properties["goldSrcServerAddress"];
				if (properties.ContainsKey("goldSrcServerPort"))    goldSrcServerPort    = int.Parse(properties["goldSrcServerPort"]);
				if (properties.ContainsKey("goldSrcServerAuth"))    goldSrcServerAuth    =           properties["goldSrcServerAuth"];
				if (properties.ContainsKey("goldSrcServerTimeout")) goldSrcServerTimeout = int.Parse(properties["goldSrcServerTimeout"]);
				                                                                                     
				if (properties.ContainsKey("sourceServerAddress")) sourceServerAddress =           properties["sourceServerAddress"];
				if (properties.ContainsKey("sourceServerPort"))    sourceServerPort    = int.Parse(properties["sourceServerPort"]);
				if (properties.ContainsKey("sourceServerAuth"))    sourceServerAuth    =           properties["sourceServerAuth"];
				if (properties.ContainsKey("sourceServerTimeout")) sourceServerTimeout = int.Parse(properties["goldSrcServerTimeout"]);
			} catch { }
		}
		
		#region Long
		
		/// <summary>
		/// This test tries to run the "cvarlist" command over RCON on a GoldSrc server
		/// </summary>
		[Test]
		public void RCONLongGoldSrcServer()
		{
			GoldSrcServer server = new GoldSrcServer(goldSrcServerAddress, goldSrcServerPort);
			server.Socket.Timeout = goldSrcServerTimeout;
			server.RconAuth(goldSrcServerAuth);
			
			string rconReply = server.RconExec("cvarlist");
			
			Assert.IsTrue(rconReply.Contains("CvarList ? for syntax"),
			              "Did not receive complete cvarlist.");
		}
		
		/// <summary>
		/// This test tries to run the "cvarlist" command over RCON on a Source server
		/// </summary>
		[Test]
		public void RCONLongSourceServer()
		{
			SourceServer server = new SourceServer(sourceServerAddress, sourceServerPort);
			server.RconSocket.Timeout = sourceServerTimeout;
			server.RconAuth(goldSrcServerAuth);
			
			string rconReply = server.RconExec("cvarlist");
			
			Assert.IsTrue(rconReply.Contains("total convars/concommands"),
			              "Did not receive complete cvarlist.");
		}
		
		#endregion
		
		#region Short
		
		/// <summary>
		/// This test tries to run the "version" command over RCON on a GoldSrc server
		/// </summary>
		[Test]
		public void RCONShortGoldSrcServer()
		{
			GoldSrcServer server = new GoldSrcServer(goldSrcServerAddress, goldSrcServerPort);
			server.Socket.Timeout = goldSrcServerTimeout;
			server.RconAuth(sourceServerAuth);
			
			string rconReply = server.RconExec("version");
			
			Assert.IsTrue(rconReply.Contains("Protocol version") &&
			              rconReply.Contains("Exe version") &&
			              rconReply.Contains("Exe build"),
			              "Did not receive correct version response.");
		}
		
		/// <summary>
		/// This test tries to run the "version" command over RCON on a Source server
		/// </summary>
		[Test]
		public void RCONShortSourceServer()
		{
			SourceServer server = new SourceServer(sourceServerAddress, sourceServerPort);
			server.RconSocket.Timeout = sourceServerTimeout;
			server.RconAuth(sourceServerAuth);
			
			string rconReply = server.RconExec("version");
			
			Assert.IsTrue(rconReply.Contains("Protocol version") &&
			              rconReply.Contains("Exe version") &&
			              rconReply.Contains("Exe build"),
			              "Did not receive correct version response.");
		}

		#endregion
		
	}
}

