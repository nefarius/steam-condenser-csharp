using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Sockets;

namespace SteamCondenser.Steam.Servers
{
	public class SourceGameServer : GameServer
	{
		public SourceGameServer(IPAddress ipAddress, int port)
			: base(ipAddress, port)
		{
			this.querySocket = new SourceServerQuerySocket(ipAddress, port);
		}

		public override void UpdateServerInfo()
		{
			this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_INFO, Encoding.ASCII.GetBytes("Source Engine Query")));

			SteamPacket packet = this.querySocket.GetReply();
			SourceServerInfoResponsePacket replyPacket = (SourceServerInfoResponsePacket)packet;

			this.serverInfo = replyPacket.ServerInfo;

			if(this.serverInfo.Port > 0)
			{
				this.gamePort = this.serverInfo.Port;
			}
			else if(this.serverInfo.ServerType != ServerTypes.Proxy)
			{
				this.gamePort = this.port;
			}

			if(this.serverInfo.SpectatorPort > 0)
			{
				this.spectatorPort = this.serverInfo.SpectatorPort;
			}
			else if(this.serverInfo.ServerType == ServerTypes.Proxy)
			{
				this.spectatorPort = this.port;
			}
		}

		public override void UpdateServerRules()
		{
			this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_RULES));

			SteamPacket packet = this.querySocket.GetReply();

			if(packet.PacketType == SteamPacketTypes.S2C_CHALLENGE)
			{
				int challengeID = ((ChallengeResponsePacket)packet).ChallengeID;

				this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_RULES, BitConverter.GetBytes(challengeID)));

				packet = this.querySocket.GetReply();
				ServerRulesResponsePacket replyPacket = (ServerRulesResponsePacket)packet;
				this.serverRules = replyPacket.ServerRules;
			}
		}

		public override void UpdatePlayerList()
		{
			this.playerList = new List<SteamPlayer>();

			if(this.serverInfo != null && this.serverInfo.ServerType != ServerTypes.Proxy)
			{
				this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_PLAYER));

				SteamPacket packet = this.querySocket.GetReply();

				if(packet.PacketType == SteamPacketTypes.S2C_CHALLENGE)
				{
					int challengeID = ((ChallengeResponsePacket)packet).ChallengeID;

					this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_PLAYER, BitConverter.GetBytes(challengeID)));

					packet = this.querySocket.GetReply();
					PlayersResponsePacket replyPacket = (PlayersResponsePacket)packet;
					this.playerList = replyPacket.PlayerList;

					/*
					// Update player count
					if(this.serverInfo.Players != this.playerList.Count)
					{
						this.serverInfo.Players = (byte)this.playerList.Count;
					}
					*/
				}
			}
		}

		public override void UpdatePing()
		{
			DateTime pingStart = DateTime.Now;
			double currentPing = 0.0;

			this.SendRequest(new SteamPacket(SteamPacketTypes.A2A_PING));

			SteamPacket packet = this.querySocket.GetReply();

			if(packet.PacketType == SteamPacketTypes.A2A_ACK)
			{
				// Update ping
				TimeSpan pingTime = DateTime.Now.Subtract(pingStart);
				currentPing = pingTime.TotalMilliseconds;

				this.pingAverage = ((this.pingAverage * this.pingCount) + currentPing) / (this.pingCount + 1);
				this.ping = currentPing;
				this.pingCount++;
			}
		}
	}
}
