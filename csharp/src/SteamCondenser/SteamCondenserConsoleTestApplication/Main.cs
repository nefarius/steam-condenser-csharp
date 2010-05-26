using System;
using System.Net;
using SteamCondenser.Steam.Servers;
using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Packets.RCON;
using System.Security;

namespace SteamCondenserConsoleTestApplication
{
	class MainClass
	{
		public static void TestInvalidGoldSrcServer()
		{
			try {
				GoldSrcServer server = new GoldSrcServer("1.0.0.0");
				int ping = server.Ping;
			} 
			catch (System.Net.Sockets.SocketException) { Console.WriteLine ("."); }
		}
		public static void TestInvalidSourceServer()
		{
		}
		public static void TestRandomGoldSrcServer()
		{
		}
		public static void TestRandomSourceServer()
		{
		}
		
		public static void Main(string[] args)
		{
		}
	}
}
