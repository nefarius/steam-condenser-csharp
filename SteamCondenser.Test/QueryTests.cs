using System;
using SteamCondenser.Steam.Servers;
using NUnit.Framework;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class QueryTests
	{

		#region Invalid Server

		/// <summary>
		/// This test tries to initialize an invalid GoldSrc server
		/// </summary>
		[Test]
		[ExpectedException(typeof(System.Net.Sockets.SocketException))]
		public void InvalidGoldSrcServer()
		{
			GoldSrcServer server = new GoldSrcServer("10.0.0.1");
			Assert.AreEqual(server.Ping, -1, "Exception should have been thrown");
		}

		/// <summary>
		/// This test tries to initialize an invalid SourceServer server
		/// </summary>
		[Test]
		[ExpectedException(typeof(System.Net.Sockets.SocketException))]
		public void InvalidSourceServer()
		{
			SourceServer server = new SourceServer("10.0.0.1");
			Assert.AreEqual(server.Ping, -1, "Exception should have been thrown");
		}

		#endregion

		#region Random Server

		/// <summary>
		/// This test gets a random GoldSrc server from the master server and
		/// does a full query on it
		/// </summary>
		[Test]
		public void GoldSrcServerList()
		{
			MasterServer masterServer = new MasterServer(MasterServer.GoldSrcMasterServer);
			masterServer.Timeout = 30000;
			var servers = masterServer.GetServers(Regions.All, "\\type\\d\\empty\\1\\full\\1\\gamedir\\valve");

			Assert.Greater(servers.Count, 0);
		}


		/// <summary>
		/// This test gets a random Source server from the master server and
		/// does a full query on it.
		/// </summary>
		[Test]
		public void SourceServerList()
		{
			MasterServer masterServer = new MasterServer(MasterServer.SourceMasterServer);
			masterServer.Timeout = 30000;
			var servers = masterServer.GetServers(Regions.All,"\\type\\d\\empty\\1\\full\\1\\gamedir\\tf");

			Assert.Greater(servers.Count, 0);
		}

		#endregion
	}
}

