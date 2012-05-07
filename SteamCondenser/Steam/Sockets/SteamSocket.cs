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
			: this(new IPEndPoint(ipAddress, port))
		{
		}

		protected SteamSocket(IPEndPoint endpoint)
		{
			// UDP client
			client = new UdpClient();

			// IP end point for server
			remoteHost = endpoint;

			// initialize buffer
			buffer = new byte[SteamPacket.PACKET_SIZE];

			// initialize reader
			bufferReader = new BinaryReader(new MemoryStream(this.buffer));
		}

		public int Timeout {
			get {
				return client.Client.ReceiveTimeout;
			}
			set {
				client.Client.ReceiveTimeout = value;
			}
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
			buffer = client.Receive(ref remoteHost);
			bytesRead = buffer.Length;

			MemoryStream memStream = new MemoryStream(buffer);
			bufferReader = new BinaryReader(memStream);

			bufferReader.BaseStream.Seek(0, SeekOrigin.Begin);
			bufferReader.BaseStream.SetLength(bytesRead);

			return bytesRead;
		}
	}
}
