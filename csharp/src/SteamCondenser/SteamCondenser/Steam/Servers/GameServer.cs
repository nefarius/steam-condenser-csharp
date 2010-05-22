using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SteamCondenser.Steam.Sockets;
using SteamCondenser.Steam.Packets;

namespace SteamCondenser.Steam.Servers
{
	public abstract class GameServer
	{
		protected IPAddress ipAddress;
		protected int port;  // current Port
		protected int gamePort;  // Port of GameServer
		protected int spectatorPort;  // Port of SpectatorServer
		protected double ping;
		protected double pingAverage;
		protected long pingCount;

		protected ServerQuerySocket querySocket;
		protected ServerInfo serverInfo;
		protected List<SteamPlayer> playerList;
		protected List<ServerRule> serverRules;

		public short ApplicationID
		{
			get { return serverInfo.ApplicationID; }
		}

		public byte BotsCount
		{
			get { return serverInfo.BotsCount; }
		}

		public string GameDescription
		{
			get { return serverInfo.GameDescription; }
		}

		public string GameDirectory
		{
			get { return serverInfo.GameDirectory; }
		}

		public string GameVersion
		{
			get { return serverInfo.GameVersion; }
		}

		public bool IsSecure
		{
			get { return serverInfo.IsSecure; }
		}

		public string MapName
		{
			get { return serverInfo.MapName; }
		}

		public byte MaxPlayers
		{
			get { return serverInfo.MaxPlayers; }
		}

		public OSTypes OSType
		{
			get { return serverInfo.OSType; }
		}

		public bool PasswordRequired
		{
			get { return serverInfo.PasswordRequired; }
		}

		public byte Players
		{
			get { return serverInfo.Players; }
		}

		public int Port
		{
			get { return port; }
		}

		public byte ProtocolVersion
		{
			get { return serverInfo.ProtocolVersion; }
		}

		public string ServerName
		{
			get { return serverInfo.ServerName; }
		}

		public ServerTypes ServerType
		{
			get { return serverInfo.ServerType; }
		}

		public int SpectatorPort
		{
			get { return spectatorPort; }
		}

		public int GamePort
		{
			get { return gamePort; }
		}

		public string SpectatorServerName
		{
			get { return serverInfo.SpectatorServerName; }
		}

		public string[] Tags
		{
			get { return serverInfo.Tags; }
		}

		public IList<ServerRule> ServerRules
		{
			get { return serverRules.AsReadOnly(); }
		}

		public IList<SteamPlayer> PlayerList
		{
			get { return playerList.AsReadOnly(); }
		}

		public IPAddress IPAddress
		{
			get { return ipAddress; }
		}

		public double Ping
		{
			get { return ping; }
		}

		public double PingAverage
		{
			get { return pingAverage; }
		}


		protected GameServer(IPAddress ipAddress, int port)
		{
			if(port < 0 || port > 65535)
			{
				throw new ArgumentException("The listening port of the server has to be a number greater than 0 and less than 65535.");
			}

			this.ipAddress = ipAddress;
			this.port = port;
		}

		public void UpdateAllInfos()
		{
			this.UpdatePing();
			this.UpdateServerInfo();
			this.UpdateServerRules();
			this.UpdatePlayerList();
		}

		public abstract void UpdateServerInfo();
		public abstract void UpdateServerRules();
		public abstract void UpdatePlayerList();
		public abstract void UpdatePing();

		protected void SendRequest(SteamPacket requestData)
		{
			this.querySocket.Send(requestData);
		}

		protected SteamPacket GetReply()
		{
			return this.querySocket.GetReply();
		}

		public static GameServer GetGameServer(IPAddress ipAddress, int port)
		{
			return new SourceGameServer(ipAddress, port);
		}
	}
}
