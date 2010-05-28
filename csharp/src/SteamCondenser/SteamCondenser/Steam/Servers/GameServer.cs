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
		private const int REQUEST_CHALLENGE = 0;
		private const int REQUEST_INFO = 1;
		private const int REQUEST_PLAYER = 2;
		private const int REQUEST_RULES = 3;

		
		protected uint challengeNumber = 0xFFFFFFFF;
		protected int ping;
		protected ServerQuerySocket querySocket;
		protected List<SteamPlayer> playerList;
		protected List<ServerRule> serverRules = new List<ServerRule>();
		protected ServerInfo serverInfo = null;
		
		protected GameServer(int portNumber)
		{
			if(portNumber < 0 || portNumber > 65535)
			{
				throw new ArgumentException("The listening port of the server has to be a number greater than 0 and less than 65535.");
			}
		}
		
		public int Ping 
		{ 
			get {
				if (ping == 0) UpdatePing();
				return ping;
			}
			protected set { ping = value; }
		}
		

		public IList<SteamPlayer> PlayerList
		{
			get {
				if (playerList == null) UpdatePlayerInfo();
				return playerList.AsReadOnly(); 
			}
		}
		
		public IList<ServerRule> ServerRules
		{
			get {
				if (this.serverRules == null) UpdateRulesInfo();
				return serverRules.AsReadOnly();
			}
		}
		
		public ServerInfo ServerInfo
		{
			get { 
				if (this.serverInfo == null) this.UpdateServerInfo();
				return serverInfo; 
			}
		}
		
		private void SendRequest(SteamPacket requestData)
		{
			this.querySocket.Send(requestData);
		}

		private SteamPacket GetReply()
		{
			return this.querySocket.GetReply();
		}		
		
		public void UpdatePing()
		{
			this.SendRequest(new SteamPacket(SteamPacketTypes.A2A_PING));
			DateTime pingStart = DateTime.Now;
			this.querySocket.GetReply();
			ping = DateTime.Now.Subtract(pingStart).Milliseconds;
		}
		
		public void UpdatePlayerInfo()
		{
			this.UpdatePlayerInfo(null);
		}
		public void UpdatePlayerInfo(string rconPassword)
		{
			handleResponseForRequest(GameServer.REQUEST_PLAYER);
		}
		
		public void UpdateRulesInfo()
		{
			this.UpdateRulesInfo(null);
		}
		public void UpdateServerInfo()
		{
			handleResponseForRequest(GameServer.REQUEST_INFO);
		}
		
		public void UpdateRulesInfo(string rconPassword)
		{
			handleResponseForRequest(GameServer.REQUEST_RULES);
		}
		public void UpdateChallengeNumber()
		{
			this.handleResponseForRequest(GameServer.REQUEST_CHALLENGE);
		}
		
		private void handleResponseForRequest(int requestType)
		{
			handleResponseForRequest(requestType, true);
		}
		private void handleResponseForRequest(int requestType, bool repeatOnFailure)
		{
			bool success = false;
			SteamPacketTypes expectedResponse;
			SteamPacket requestPacket = null;
			switch (requestType) 
			{
			case GameServer.REQUEST_CHALLENGE:
				expectedResponse = SteamPacketTypes.S2C_CHALLENGE;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_SERVERQUERY_GETCHALLENGE);
				break;
			case GameServer.REQUEST_INFO:
				expectedResponse = SteamPacketTypes.S2A_INFO;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_INFO);
				break;
			case GameServer.REQUEST_PLAYER:
				expectedResponse = SteamPacketTypes.S2A_PLAYER;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_PLAYER, challengeNumber.ReverseBytes().GetBytes());
				break;
			case GameServer.REQUEST_RULES:
				expectedResponse = SteamPacketTypes.S2A_RULES;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_RULES, challengeNumber.ReverseBytes().GetBytes());
				break;
			default:
				throw new SteamCondenserException("Called with wrong request type.");
			}
			
			this.SendRequest(requestPacket);
			
			success = false;
			do
			{
				SteamPacket responsePacket = this.GetReply();
				Console.WriteLine ("Response: {0}", responsePacket.PacketType);
				success = (responsePacket.PacketType == expectedResponse);
				
				if (responsePacket is SourceServerInfoResponsePacket)
				{
					success = true;
					this.serverInfo = (responsePacket as SourceServerInfoResponsePacket).ServerInfo;
				}
				/*
				if (responsePacket.GetType().IsSubclassOf(typeof(ServerInfoBasePacket)))
				{
					success = true;
				}
				*/
				else if (responsePacket is PlayersResponsePacket)
				{
					playerList = (responsePacket as PlayersResponsePacket).PlayerList;
				}
				else if (responsePacket is ChallengeResponsePacket)
				{
					this.challengeNumber = (uint)(responsePacket as ChallengeResponsePacket).ChallengeID;
				}
				else if (responsePacket is ServerRulesResponsePacket)
				{
					this.serverRules = (responsePacket as ServerRulesResponsePacket).ServerRules;
				}
				
			} while (repeatOnFailure && !success);
			
			
		}


	}
	/*
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
	*/
}
