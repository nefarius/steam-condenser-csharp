using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using SteamCondenser.Steam.Packets;

namespace SteamCondenser.Steam.Sockets
{
	public abstract class SteamSocket
	{
		protected UdpClient client;
		protected IPEndPoint remoteHost;

		protected byte[] buffer;
		protected BinaryReader bufferReader;

		protected SteamSocket(IPAddress ipAddress, int port)
		{
			// UDP client
			this.client = new UdpClient();

			// IP end point for server
			this.remoteHost = new IPEndPoint(ipAddress, port);

			// initialize buffer
			this.buffer = new byte[SteamPacket.PACKET_SIZE];

			// initialize reader
			this.bufferReader = new BinaryReader(new MemoryStream(this.buffer));

		}

		protected SteamPacket CreatePacket()
		{
			byte[] packetData = new byte[SteamPacket.PACKET_SIZE];
			this.buffer.CopyTo(packetData, 0);
			packetData = this.bufferReader.ReadBytes(SteamPacket.PACKET_SIZE);
			//this.bufferReader.Read(packetData, (int)this.bufferReader.BaseStream.Position, SteamPacket.PACKET_SIZE);

			return SteamPacket.CreatePacket(packetData);
		}

	}
}
