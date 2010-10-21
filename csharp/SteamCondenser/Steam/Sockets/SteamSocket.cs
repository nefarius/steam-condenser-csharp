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
			byte[] packetData = this.bufferReader.ReadBytes(buffer.Length - (int)bufferReader.BaseStream.Position);
			
			//byte[] packetData = this.bufferReader.ReadBytes(SteamPacket.PACKET_SIZE);
			
			return SteamPacket.CreatePacket(packetData);
		}

		public abstract SteamPacket GetReply();
		
		protected int ReceivePacket(int bufferLength)
		{
			int bytesRead;

			// receive data
			this.buffer = this.client.Receive(ref this.remoteHost);
			bytesRead = this.buffer.Length;

			MemoryStream memStream = new MemoryStream(this.buffer);
			this.bufferReader = new BinaryReader(memStream);

			this.bufferReader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
			this.bufferReader.BaseStream.SetLength(bytesRead);

			return bytesRead;
		}
		


	}
}
