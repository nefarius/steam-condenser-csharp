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
			: base(ipAddress, port)
		{
			this.client.Connect(this.remoteHost);

			this.client.DontFragment = true;
			this.client.Client.ReceiveTimeout = 10000;
		}

		protected bool PacketIsSplit()
		{
			int splitCheck = this.bufferReader.ReadInt32();
			return (splitCheck == SteamPacket.PACKET_SPLIT_MARKER);
		}

		public void Send(SteamPacket dataPacket)
		{
			byte[] byteData = dataPacket.GetBytes();

			this.client.Send(byteData, byteData.Length);
		}

		public abstract SteamPacket GetReply();

		protected int ReceivePacket()
		{
			return this.ReceivePacket(0);
		}

		protected int ReceivePacket(int bufferLength)
		{
			int bytesRead;

			if(bufferLength == 0)
			{
				this.buffer.Initialize();  // clear
			}
			else
			{
				this.buffer = new byte[bufferLength];
			}

			// receive data
			this.buffer = this.client.Receive(ref this.remoteHost);
			bytesRead = this.buffer.Length;

			MemoryStream memStream = new MemoryStream(this.buffer);
			this.bufferReader = new BinaryReader(memStream);

			this.bufferReader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
			this.bufferReader.BaseStream.SetLength(bytesRead);

			return bytesRead;

		}

		public void Dispose()
		{
			this.client.Close();
		}
	}
}
