using System;
using System.Net;
using SteamCondenser.Steam.Sockets;
using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Packets.RCON;

namespace SteamCondenser.Steam.Sockets
{
	public class RCONSocket : SteamSocket
	{
		public RCONSocket(IPAddress ipAddress, int portNumber)
			: base(ipAddress, portNumber)
		{
			this.client.Connect(this.remoteHost);
		}
		
		public void Send(RCONPacket dataPacket)
		{
			byte[] byteData = dataPacket.GetBytes(true);
			
			this.client.Send(byteData, byteData.Length);
		}	
		
		public override SteamPacket GetReply()
		{
			if (this.ReceivePacket(1440) <= 0) {
				throw new RCONBanException();
			}
			
			int packetSize = this.bufferReader.ReadInt32() + 4;
			if (packetSize > 1440) {
				throw new Exception("Invalid packet size");
			}
			
			return RCONPacket.CreatePacket(buffer);
		}
	}
}
