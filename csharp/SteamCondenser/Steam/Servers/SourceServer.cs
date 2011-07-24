using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Packets.RCON;
using SteamCondenser.Steam.Sockets;

namespace SteamCondenser.Steam.Servers
{
	public class SourceServer : GameServer
	{
		public ServerQuerySocket Socket {
			get {
				return this.querySocket;
			}
		}
		
		public RCONSocket RconSocket {
			get {
				return this.rconSocket;
			}
		}
		
		protected RCONSocket rconSocket;
		protected int rconRequestId;
		
		public SourceServer(string ipAddress)
			: this(ipAddress, 27015)
		{
		}
		
		public SourceServer(string ipAddress, int portNumber)
			: this(IPAddress.Parse(ipAddress), portNumber)
		{
		}
		
		public SourceServer(IPAddress ipAddress)
			: this(ipAddress, 27015)
		{
		}
		
		public SourceServer(IPEndPoint endpoint)
			: this(endpoint.Address, endpoint.Port)
		{
		}
		
		public SourceServer(IPAddress ipAddress, int portNumber)
			: base(portNumber)
		{
			this.querySocket = new SourceServerQuerySocket(ipAddress, portNumber);
			this.rconSocket = new RCONSocket(ipAddress, portNumber);
		}
		
		
		public bool RconAuth(string password)
		{
			this.rconRequestId = (new Random()).Next();
			this.rconSocket.Send(new RCONAuthRequestPacket(this.rconRequestId, password));
			RCONAuthResponsePacket reply = this.rconSocket.GetReply() as RCONAuthResponsePacket;
			return (reply.RequestId == this.rconRequestId);
		}
		
		public string RconExec(string command)
		{
			this.rconSocket.Send(new RCONExecRequestPacket(this.rconRequestId, command));
			List<RCONExecResponsePacket> responsePackets=  new List<RCONExecResponsePacket>();
			RCONPacket responsePacket;
			
			try {
				while (true) {
					responsePacket = (RCONPacket)this.rconSocket.GetReply();
					if (responsePacket is RCONAuthResponsePacket)
					{
						throw new RCONNoAuthException();
					}
					responsePackets.Add((RCONExecResponsePacket)responsePacket);
				}
			} catch {
				if (responsePackets.Count == 0) throw new Exception();
			}
			
			string response = "";
			return "";
		}
		
	}
}