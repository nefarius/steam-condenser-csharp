using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SteamCondenser.Steam.Packets;
using System.IO;


namespace SteamCondenser.Steam.Sockets
{
	public abstract class ServerQuerySocket : SteamSocket
	{
		public ServerQuerySocket(IPAddress ipAddress, int port)
			: this(new IPEndPoint(ipAddress, port))
		{
		}

		public ServerQuerySocket(IPEndPoint endpoint)
			: base(endpoint)
		{
			this.client.Connect(this.remoteHost);

			this.client.DontFragment = true;
		}

		protected bool PacketIsSplit()
		{
			int splitCheck = this.bufferReader.ReadInt32();
			return (splitCheck == SteamPacket.PACKET_SPLIT_MARKER);
		}

		protected int ReceivePacket()
		{
			return this.ReceivePacket(0);
		}

		public void Send(SteamPacket dataPacket)
		{
			byte[] byteData = dataPacket.GetBytes(true);
			this.client.Send(byteData, byteData.Length);
		}

		public void Dispose()
		{
			this.client.Close();
		}
	}
}
