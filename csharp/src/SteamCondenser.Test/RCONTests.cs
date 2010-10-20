/**
* This code is free software; you can redistribute it and/or modify it under
* the terms of the new BSD License.
*
* Copyright (c) 2009-2010, Andrius Bentkus
*/ 

using System;
using SteamCondenser.Steam.Servers;
using NUnit.Framework;

namespace SteamCondenser.Test
{
	public class RCONTests
	{
		static string goldSrcServerAddress = "127.0.0.1";
		static int    goldSrcServerPort    = 27015;
		static string goldSrcServerAuth    = "test";
		
		static string sourceServerAddress  = "127.0.0.1";
		static int    sourceServerPort     = 27015;
		static string sourceServerAuth     = "test";
		
		/// <summary>
		/// Setup method to load fixtures.
		/// </summary>
		static RCONTests()
		{
		}
		
		#region Long
		
		/// <summary>
		/// This test tries to run the "cvarlist" command over RCON on a GoldSrc server
		/// </summary>
		[Test]
		public void RCONLongGoldSrcServer()
		{
			GoldSrcServer server = new GoldSrcServer(goldSrcServerAddress, goldSrcServerPort);
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
			GoldSrcServer server = new GoldSrcServer(sourceServerAddress, sourceServerPort);
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
		public void RRCONShortSourceServer()
		{
			GoldSrcServer server = new GoldSrcServer(sourceServerAddress, sourceServerPort);
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

