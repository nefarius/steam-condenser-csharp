using System;
using System.Net;

using SteamCondenser.Steam.Packets;

namespace SteamCondenser.Steam.Sockets
{
	public class MasterServerSocket : ServerQuerySocket
	{
		public MasterServerSocket(IPAddress ipAddress, int port)
			: this(new IPEndPoint(ipAddress, port))
		{
		}

		public MasterServerSocket(IPEndPoint endpoint)
			: base(endpoint)
		{
		}

		public override SteamPacket GetReply()
		{
			this.ReceivePacket(1500);
			if (this.bufferReader.ReadUInt32() != 0xFFFFFFFF) {
				throw new Exception("Master query response has wrong packet header.");
			}
			return this.CreatePacket();
		}
	}
}
